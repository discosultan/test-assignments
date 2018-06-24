using Akka.Actor;
using System;
using System.Collections.Generic;
using static Gradilium.ShoppingBasket.Products.ProductActor;

namespace Gradilium.ShoppingBasket.Products
{
    public class ProductsManagerActor : UntypedActor
    {
        // Keeps a local copy of products for quick retrieval. Good idea?
        readonly List<Product> _products = new List<Product>();

        protected override void OnReceive(object message)
        {
            switch (message)
            {
                case AddProduct cmd: HandleAddProduct(cmd); break;
                case GetProducts cmd: HandleGetProducts(cmd); break;
                case ProductCommand cmd: HandleProductCommand(cmd); break;
            }
        }

        // Handlers.

        void HandleAddProduct(AddProduct cmd)
        {
            var productIdStr = cmd.Product.Id.ToString();
            if (Context.Child(productIdStr) is Nobody)
            {
                Context.ActorOf(Props.Create<ProductActor>(cmd.Product), productIdStr);
                _products.Add(cmd.Product);
                Sender.Tell(true);
            }
            else
            {
                // TODO: Handle product already exists.
                Sender.Tell(false);
                throw new NotImplementedException();
            }
        }

        void HandleGetProducts(GetProducts _)
        {
            Sender.Tell(_products);
        }

        void HandleProductCommand(ProductCommand cmd)
        {
            var productIdStr = cmd.ProductId.ToString();
            var child = Context.Child(productIdStr);
            if (child is Nobody)
                // TODO: Handle product does not exist.
                throw new NotImplementedException();
            child.Forward(cmd);
        }

        // Commands.

        public class AddProduct
        {
            public Product Product { get; set; }
        }

        public class GetProducts { }
    }
}
