using Gradilium.ShoppingBasket.Baskets;
using System;
using Xunit;

namespace Gradilium.ShoppingBasket.UnitTests
{
    public class ItemTest
    {
        [Fact]
        public void NewItem_ValidInput_Success()
        {
            var productId = Guid.NewGuid();
            var item = new Item(productId, 1);

            Assert.Equal(productId, item.ProductId);
            Assert.Equal(1, item.Count);
        }

        [Fact]
        public void AddRemove_ValidInput_Success()
        {
            var item = new Item(Guid.NewGuid(), 0);

            var item1 = item + 2;
            var item2 = item1 - 1;

            Assert.Equal(2, item1.Count);
            Assert.Equal(1, item2.Count);
        }
    }
}
