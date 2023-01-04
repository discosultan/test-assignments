/*
The goal of this project to implement an API service for answering questions about user balances in a blockchain.
The API service is fed data from the blockchain by subscribing to an event stream. The system calls an endpoint on
the API service to provide it with the following data every time a new “block” is produced:

{
  "block_id": "0000000000000000000bd4d53c0d75c3eb3c764997bc1df2a094225a16f80d1f",
  "parent_id": "0000000000000000000516018f9a36906dc6e777b6dca5ac61a48525f9172329",
  "transactions": [
    {
      "txid": "fa6b62c3051647b9314509e3e3a62d2c7489f37f72a471315134e0e512ad227d",
      "from": "Alice",
      "to": "Bob",
      "amount": 100
    },
    {
      "txid": "77a57e8c21f5cb201b4073433583a3dc57fd1eaef7ba6ad5945f7b1711ca57bc",
      "to": "Bob",
      "amount": 100
    },
  ],
}

Blockchains also have the ability to fork, for example, block D could be a descendant of block B:

A → B → C
  \_> D

The current “active view”, or canonical fork, is determined by whichever fork is “longest” (this is the same as the block with the longest chain of parents).

The goal of this scenario is to design an API service that accepts those block events and exposes an interface for requests of the form “What is Alice’s current balance?”

*/

mod api;
mod block;
mod storage;

use anyhow::anyhow;
use clap::Parser;

use storage::{lmdb, memory, Storage};

#[derive(Parser)]
struct Args {
    #[arg(help="Storage backend to use; supported values: \"memory\", \"lmdb\".")]
    storage: String,
}

fn test(storage: Box<dyn Storage>, test: block::Test) {
    let block_server = block::Server::new(test);
    let mut api_server = api::Server::new(storage);
    let mut errors = Vec::new();
    block_server.subscribe(|block| {
        if let Err(error) = api_server.submit_block(block) {
            errors.push(error);
        }
    });
    println!(
        "Alice's balance: {:?}; errors: {:?}",
        api_server.get_user_balance("Alice"),
        errors
    );
}

fn main() -> anyhow::Result<()> {
    let args = Args::parse();

    let storage: Box<dyn Storage> = match args.storage.as_ref() {
      "memory" => Box::new(memory::Storage::new()),
      "lmdb" => Box::new(lmdb::Storage::new()),
      storage_type => {
        return Err(anyhow!(format!("Invalid storage: {}", storage_type)))
      }
    };

    // test1 consists of a single block
    test(storage, block::Test::Test1);
    // test2 consists of a single chain of blocks
    test(storage, block::Test::Test2);
    // test3 consists of multiple forks
    test(storage, block::Test::Test3);
    // test4 consists of multiple forks where the longest chain changes mid stream
    test(storage, block::Test::Test4);

    Ok(())
}
