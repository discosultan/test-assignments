use lmdb::{DatabaseFlags, Environment, Transaction, WriteFlags, Database};
use tempdir::TempDir;

use crate::block::BlockID;

use super::{BlockLength, BlockState};

const LONGEST_HEAD_KEY: &'static str = "longest_head";

pub struct Storage {
    db: Database,
    env: Environment,
}

impl Storage {
    pub fn new() -> Self {
        let tmp = TempDir::new("blockchain-explorer").expect("failed to open tmpdir");
        let path = tmp.path();

        let mut builder = Environment::new();
        builder.set_max_dbs(16);

        let env = builder.open(path).expect("failed to open env");
        let db = env
            .create_db(Some("mydb"), DatabaseFlags::empty())
            .expect("failed to open db");

        Self { db, env }
    }
}

impl super::Storage for Storage {
    fn get_block_state(&self, id: &BlockID) -> Option<BlockState> {
        let rotxn = self.env.begin_ro_txn().expect("can't begin ro txn");
        match rotxn.get(self.db, id) {
            Ok(rbytes) => {
                let rstr = std::str::from_utf8(rbytes).expect("failed to parse read bytes");
                let rval: BlockState = serde_json::from_str(rstr).expect("failed to deserialize");
                Some(rval)
            }
            Err(_) => {
                // TODO: Handle error to distinguish between missing value and an actual error.
                None
            }
        }
    }

    fn set_block_state(&mut self, id: &BlockID, state: BlockState) {
        let mut rwtxn = self.env.begin_rw_txn().expect("can't begin rw txn");

        let wbytes = serde_json::to_string(&state).expect("failed to serialize");

        rwtxn
            .put(self.db, id, &wbytes, WriteFlags::empty())
            .expect("put failed");
        rwtxn.commit().expect("commit failed for rwtxn");
    }

    fn get_longest_head(&self) -> Option<BlockLength> {
        let rotxn = self.env.begin_ro_txn().expect("can't begin ro txn");
        match rotxn.get(self.db, &LONGEST_HEAD_KEY) {
            Ok(rbytes) => {
                let rstr = std::str::from_utf8(rbytes).expect("failed to parse read bytes");
                let rval: BlockLength = serde_json::from_str(rstr).expect("failed to deserialize");
                Some(rval)
            }
            Err(_) => {
                // TODO: Handle error to distinguish between missing value and an actual error.
                None
            }
        }
    }

    fn set_longest_head(&mut self, head: BlockLength) {
        let mut rwtxn = self.env.begin_rw_txn().expect("can't begin rw txn");

        let wbytes = serde_json::to_string(&head).expect("failed to serialize");

        rwtxn
            .put(self.db, &LONGEST_HEAD_KEY, &wbytes, WriteFlags::empty())
            .expect("put failed");
        rwtxn.commit().expect("commit failed for rwtxn");
    }
}
