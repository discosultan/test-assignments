using Gradilium.ShoppingBasket.Products;
using System;
using Xunit;

namespace Gradilium.ShoppingBasket.UnitTests
{
    public class ProductTest
    {
        [Fact]
        public void NewProduct_ValidInput_Success()
        {
            var product = new Product("name", "manufacturer", 1.0m, 1);

            Assert.Equal("name", product.Name);
            Assert.Equal("manufacturer", product.Manufacturer);
            Assert.Equal(1.0m, product.Price);
            Assert.Equal(1, product.Count);
        }

        [Fact]
        public void NewProduct_NullInput_ArgumentNullEx()
        {
            Assert.Throws<ArgumentNullException>(() => new Product(null, "manufacturer", 1.0m, 1));
            Assert.Throws<ArgumentNullException>(() => new Product("name", null, 1.0m, 1));
        }

        [Fact]
        public void NewProduct_NegativeInput_InvalidOpEx()
        {
            Assert.Throws<InvalidOperationException>(() => new Product("name", "manufacturer", -1.0m, 1));
            Assert.Throws<InvalidOperationException>(() => new Product("name", "manufacturer", 1.0m, -1));
        }

        [Fact]
        public void UpdateStock_ByLargeAmount_WithinLimits()
        {
            var product = new Product("name", "manufacturer", 1.0m, 0);

            product.UpdateStock(2);
            var count1 = product.Count;
            product.UpdateStock(-1);
            var count2 = product.Count;
            product.UpdateStock(Product.MaxCount + 1);
            var count3 = product.Count;
            product.UpdateStock(-Product.MaxCount - 1);
            var count4 = product.Count;

            Assert.Equal(2, count1);
            Assert.Equal(1, count2);
            Assert.Equal(Product.MaxCount, count3);
            Assert.Equal(0, count4);
        }
    }
}
