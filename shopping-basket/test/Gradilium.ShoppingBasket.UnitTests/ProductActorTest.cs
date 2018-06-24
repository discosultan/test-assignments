using Akka.Actor;
using Akka.TestKit.Xunit2;
using Gradilium.ShoppingBasket.Products;
using Xunit;
using static Gradilium.ShoppingBasket.Products.ProductActor;

namespace Gradilium.ShoppingBasket.UnitTests
{
    public class ProductActorTest : TestKit
    {
        [Fact]
        public void GetProduct_Success()
        {
            var expectedProduct = new Product("name", "manufacturer", 1.0m, 1);
            var actor = ActorOf(Props.Create<ProductActor>(expectedProduct));

            actor.Tell(new GetProduct());
            var product = ExpectMsg<Product>();

            Assert.Equal(expectedProduct, product);
        }
    }
}
