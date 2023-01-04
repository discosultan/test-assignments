use std::collections::{
    hash_map::Entry::{Occupied, Vacant},
    HashMap,
};

use crate::{
    block::{Block, BlockID, Transactions},
    storage::{Storage, BlockState},
};

#[derive(thiserror::Error, Debug)]
pub enum SubmitBlockError {
    #[error("Received a block that has already been processed.")]
    AlreadyProcessed(BlockID),
    // TODO: If blocks can be submitted out-of-order, we could buffer the block instead and apply
    // it once we receive the parent.
    #[error("Received a block for which a parent is missing.")]
    MissingParent { child: BlockID, parent: BlockID },
    #[error("Block contains an invalid transaction.")]
    InvalidTransaction,
}

pub struct Server {
    storage: Box<dyn Storage>,
}

impl Server {
    pub fn new(storage: Box<dyn Storage>) -> Self {
        Self { storage }
    }

    /// Persists a block on the server.
    pub fn submit_block(&mut self, block: &Block) -> Result<(), SubmitBlockError> {
        // Current implementation favours balance access speed over storage. We use up a lot of
        // storage for tracking balances at every block. This doesn't scale as well as storing
        // raw block data and calculating state on-demand.

        if let Some(_) = self.storage.get_block_state(&block.block_id) {
            return Err(SubmitBlockError::AlreadyProcessed(block.block_id.clone()));
        }

        let length = match block.parent_id {
            None => {
                let length = 1;
                self.storage.set_block_state(
                    &block.block_id,
                    BlockState {
                        length,
                        balances: update_balances(&HashMap::new(), &block.transactions)?,
                    },
                );
                length
            }
            Some(ref parent_id) => match self.storage.get_block_state(parent_id) {
                None => {
                    return Err(SubmitBlockError::MissingParent {
                        parent: parent_id.clone(),
                        child: block.block_id.clone(),
                    });
                }
                Some(parent) => {
                    let length = parent.length + 1;
                    let node = BlockState {
                        length,
                        balances: update_balances(&parent.balances, &block.transactions)?,
                    };
                    self.storage.set_block_state(&block.block_id, node);
                    length
                }
            },
        };

        self.try_update_longest_head(&block.block_id, length);

        Ok(())
    }

    /// Gets user balance as of currently processed blockchain state. Returns `None` if there are
    /// no associated transactions with the user `user`.
    pub fn get_user_balance(&self, user: &str) -> Option<u64> {
        match &self.storage.get_longest_head() {
            None => None,
            Some((block_id, _)) => match self.storage.get_block_state(block_id) {
                None => unreachable!(),
                Some(node) => node.balances.get(user).cloned(),
            },
        }
    }

    fn try_update_longest_head(&mut self, new_block_id: &BlockID, new_length: usize) {
        // Updates the longest head if the new block height is longer.
        // If multiple blocks all have longest length, the first one will be considered longest
        // head.
        match &self.storage.get_longest_head() {
            None => self.storage.set_longest_head((new_block_id.clone(), new_length)),
            Some((_, length)) => {
                if new_length > *length {
                    self.storage.set_longest_head((new_block_id.clone(), new_length));
                }
            }
        }
    }
}

/// Converts transactions to balances state. Returns `None` if invalid value. For example, if
/// balance goes negative.
fn update_balances(
    balances: &HashMap<String, u64>,
    transactions: &[Transactions],
) -> Result<HashMap<String, u64>, SubmitBlockError> {
    let mut balances = balances.clone();
    // TODO: Should we handle all minting transactions before transfers?
    for transaction in transactions {
        match transaction {
            // TODO: Error if out of bounds of u64.
            Transactions::Mint {
                txid: _,
                to,
                ref amount,
            } => *balances.entry(to.to_owned()).or_default() += amount,
            Transactions::Transfer {
                txid: _,
                from,
                to,
                ref amount,
            } => {
                match balances.entry(from.to_owned()) {
                    Vacant(_) => {
                        // Negative balance.
                        return Err(SubmitBlockError::InvalidTransaction);
                    }
                    Occupied(mut entry) => {
                        match entry.get().checked_sub(*amount) {
                            None => {
                                // Negative balance.
                                return Err(SubmitBlockError::InvalidTransaction);
                            }
                            Some(value) => {
                                entry.insert(value);
                            }
                        }
                    }
                }
                *balances.entry(to.to_owned()).or_default() += amount;
            }
        }
    }
    Ok(balances)
}
