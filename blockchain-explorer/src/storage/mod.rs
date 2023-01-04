use std::collections::HashMap;

use serde::{Deserialize, Serialize};

use crate::block::BlockID;

pub mod lmdb;
pub mod memory;

// TODO: Error handling.
pub trait Storage {
    fn get_block_state(&self, id: &BlockID) -> Option<BlockState>;
    fn set_block_state(&mut self, id: &BlockID, state: BlockState);
    fn get_longest_head(&self) -> Option<BlockLength>;
    fn set_longest_head(&mut self, head: BlockLength);
}

// TODO: We could optimize a little by storing an `&BlockID` instead.
pub type BlockLength = (BlockID, usize);

#[derive(Clone, Serialize, Deserialize)]
pub struct BlockState {
    pub length: usize,
    // TODO: In case user names can become large, we could optimize by storing the names in a
    //       separate collection and using `&str` here.
    pub balances: HashMap<String, u64>,
}
