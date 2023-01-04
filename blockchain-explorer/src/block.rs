pub type BlockID = String;
pub type TransactionID = String;

pub struct Block {
    pub block_id: BlockID,
    pub parent_id: Option<BlockID>,
    pub transactions: Vec<Transactions>,
}

pub enum Transactions {
    Transfer {
        txid: TransactionID,
        from: String,
        to: String,
        amount: u64,
    },

    Mint {
        txid: TransactionID,
        to: String,
        amount: u64,
    },
}

pub enum Test {
    Test1,
    Test2,
    Test3,
    Test4,
}

pub struct Server {
    test: Test,
}

impl Server {
    pub fn new(t: Test) -> Self {
        Self { test: t }
    }

    pub fn subscribe<F: FnMut(&Block)>(&self, mut f: F) {
        let mut blocks: Vec<Block> = Vec::new();

        let block_a: Block = Block {
            block_id: "A".to_string(),
            parent_id: None,
            transactions: vec![
                // Old input.
                // Transactions::Transfer {
                //     txid: "A0".to_string(),
                //     from: "Alice".to_string(),
                //     to: "Bob".to_string(),
                //     amount: 100,
                // },
                // Transactions::Mint {
                //     txid: "A1".to_string(),
                //     to: "Bob".to_string(),
                //     amount: 100,
                // },
                // New input.
                Transactions::Mint {
                    txid: "A0".to_string(),
                    to: "Bob".to_string(),
                    amount: 100,
                },
                Transactions::Transfer {
                    txid: "A1".to_string(),
                    from: "Bob".to_string(),
                    to: "Alice".to_string(),
                    amount: 100,
                },
            ],
        };

        let block_b: Block = Block {
            block_id: "B".to_string(),
            parent_id: Some("A".to_string()),
            transactions: vec![
                // Old input.
                // Transactions::Transfer {
                //     txid: "B0".to_string(),
                //     from: "Bob".to_string(),
                //     to: "Alice".to_string(),
                //     amount: 50,
                // }
                // New input.
                Transactions::Transfer {
                    txid: "B0".to_string(),
                    from: "Alice".to_string(),
                    to: "Bob".to_string(),
                    amount: 50,
                }
            ],
        };

        let block_c: Block = Block {
            block_id: "C".to_string(),
            parent_id: Some("A".to_string()),
            transactions: vec![
                // Old input.
                // Transactions::Transfer {
                //     txid: "C0".to_string(),
                //     from: "Bob".to_string(),
                //     to: "Alice".to_string(),
                //     amount: 20,
                // }
                // New input.
                Transactions::Transfer {
                    txid: "C0".to_string(),
                    from: "Alice".to_string(),
                    to: "Bob".to_string(),
                    amount: 20,
                }
            ],
        };

        let block_d: Block = Block {
            block_id: "D".to_string(),
            parent_id: Some("C".to_string()),
            transactions: vec![Transactions::Transfer {
                txid: "D0".to_string(),
                from: "Alice".to_string(),
                to: "Bob".to_string(),
                amount: 50,
            }],
        };

        match self.test {
            Test::Test1 => {
                blocks.push(block_a);
            }
            Test::Test2 => {
                blocks.push(block_a);
                blocks.push(block_b);
            }
            Test::Test3 => {
                blocks.push(block_a);
                blocks.push(block_b);
                blocks.push(block_c);
            }
            Test::Test4 => {
                blocks.push(block_a);
                blocks.push(block_b);
                blocks.push(block_c);
                blocks.push(block_d);
            }
        }

        for block in &blocks {
            f(block);
        }
    }
}
