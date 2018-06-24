using Akka.Actor;
using System;

namespace Gradilium.ShoppingBasket.Products
{
    public class ProductActor : UntypedActor
    {
        readonly Product _product;

        public ProductActor(Product product)
        {
            _product = product;
        }

        protected override void OnReceive(object message)
        {
            switch (message)
            {
                case GetProduct cmd: Sender.Tell(_product); break;
                case UpdateStock cmd: Sender.Tell(_product.UpdateStock(cmd.Count)); break;
            }
        }

        // Commands.

        public abstract class ProductCommand
        {
            public Guid ProductId { get; set; }
        }

        public class GetProduct : ProductCommand { }

        public class UpdateStock : ProductCommand
        {
            public int Count { get; set; }
        }
    }
}
