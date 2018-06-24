using Akka.Actor;
using Gradilium.ShoppingBasket.Products;
using System;
using static Gradilium.ShoppingBasket.Products.ProductActor;

namespace Gradilium.ShoppingBasket.Baskets
{
    public class BasketActor : UntypedActor
    {
        readonly Basket _basket = new Basket();

        readonly IActorRef _productsManager;

        public BasketActor(IActorRef productsManager)
        {
            _productsManager = productsManager;
        }

        protected override void OnReceive(object message)
        {
            switch (message)
            {
                case GetBasket cmd:
                    HandleGetBasket(cmd);
                    break;
                case AddToBasket cmd when cmd.Item.Count > 0:
                    HandleAddToBasket(cmd);
                    break;
                case RemoveFromBasket cmd when cmd.Item.Count > 0 && _basket.Items.ContainsKey(cmd.Item.ProductId):
                    HandleRemoveFromBasket(cmd);
                    break;
                case AddToBasket _:
                case RemoveFromBasket _:
                    HandleInvalidInput();
                    break;
            }
        }

        // Handlers.

        void HandleGetBasket(GetBasket _)
        {
            Sender.Tell(_basket);
        }

        void HandleAddToBasket(AddToBasket cmd)
        {
            if (!_basket.Items.TryGetValue(cmd.Item.ProductId, out Item item))
            {
                item = new Item(cmd.Item.ProductId, 0);
                _basket.Items[cmd.Item.ProductId] = item;
            }
            UpdateBasketItem(item, cmd.Item.Count);
        }

        void HandleRemoveFromBasket(RemoveFromBasket cmd)
        {
            UpdateBasketItem(_basket.Items[cmd.Item.ProductId], -cmd.Item.Count);
        }

        void UpdateBasketItem(Item existing, int by)
        {
            var desiredUpdateBy = existing.GetMaxAllowedChange(by, Product.MaxCount);

            if (desiredUpdateBy == 0)
            {
                Sender.Tell(0);
            }
            else
            {
                _productsManager.Ask<int>(new UpdateStock
                {
                    ProductId = existing.ProductId,
                    Count = -desiredUpdateBy
                }).ContinueWith(task =>
                {
                    var numModified = task.Result;
                    _basket.Items[existing.ProductId] = existing -= numModified;
                    return Math.Abs(numModified);
                }).PipeTo(Sender);
            }
        }

        void HandleInvalidInput()
        {
            Sender.Tell(0);
        }

        // Commands.

        public abstract class UserCommand
        {
            public Guid UserId { get; set; }
        }

        public class GetBasket : UserCommand { }

        public class AddToBasket : UserCommand
        {
            public Item Item { get; set; }
        }

        public class RemoveFromBasket : UserCommand
        {
            public Item Item { get; set; }
        }
    }
}
