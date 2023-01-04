use std::collections::HashMap;

use crate::block::BlockID;

use super::{BlockLength, BlockState};

pub struct Storage {
    blocks: HashMap<BlockID, BlockState>,
    // TODO: We could optimize a little by storing an `&BlockID` instead.
    longest_head: Option<BlockLength>,
}

impl Storage {
    pub fn new() -> Self {
        Self {
            blocks: HashMap::new(),
            longest_head: None,
        }
    }
}

impl super::Storage for Storage {
    fn get_block_state(&self, id: &BlockID) -> Option<BlockState> {
        self.blocks.get(id).cloned()
    }

    fn set_block_state(&mut self, id: &BlockID, state: BlockState) {
        self.blocks.insert(id.clone(), state);
    }

    fn get_longest_head(&self) -> Option<BlockLength> {
        self.longest_head.clone()
    }

    fn set_longest_head(&mut self, head: BlockLength) {
        self.longest_head = Some(head);
    }
}
