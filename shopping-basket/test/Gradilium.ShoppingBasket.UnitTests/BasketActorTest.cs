using Akka.Actor;
using Akka.TestKit.Xunit2;
using Gradilium.ShoppingBasket.Baskets;
using Gradilium.ShoppingBasket.Products;
using System;
using System.Linq;
using Xunit;
using static Gradilium.ShoppingBasket.Baskets.BasketActor;
using static Gradilium.ShoppingBasket.Products.ProductsManagerActor;

namespace Gradilium.ShoppingBasket.UnitTests
{
    public class BasketActorTest : TestKit
    {
        [Fact]
        public void GetBasket_NoBasket_NewEmptyBasket()
        {
            var actor = ActorOf(Props.Create<BasketActor>(new object[] { null }));

            actor.Tell(new GetBasket());
            var basket = ExpectMsg<Basket>();

            Assert.Empty(basket.Items);
        }

        [Theory]
        [InlineData(2, 1, 1, 1)] // Happy flow.
        [InlineData(1, 2, 1, 0)] // More added than available.
        [InlineData(1, 0, 0, 1)] // None added.
        public void AddToBasket_AsMuchAsPossibleAdded(
            int availableStock, int addToBasket, int addedToBasket, int remainingStock)
        {
            var productsManager = ActorOf(Props.Create<ProductsManagerActor>());
            var actor = ActorOf(Props.Create<BasketActor>(new object[] { productsManager }));

            var userId = Guid.NewGuid();
            var product = new Product("name", "manufacturer", 1.0m, availableStock);

            productsManager.Tell(new AddProduct { Product = product });
            ExpectMsg<bool>();

            actor.Tell(new AddToBasket
            {
                UserId = userId,
                Item = new Item(product.Id, addToBasket)
            });
            var numItemsAddedToBasket = ExpectMsg<int>();

            actor.Tell(new GetBasket { UserId = userId });
            var basket = ExpectMsg<Basket>();

            Assert.Equal(addedToBasket, numItemsAddedToBasket);
            Assert.Equal(remainingStock, product.Count);
            if (addedToBasket > 0)
                Assert.Contains(product.Id, basket.Items.Values.Select(item => item.ProductId));
        }
    }
}
