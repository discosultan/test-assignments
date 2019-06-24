using System;
using Xunit;

namespace AlbumPrinter.UnitTests
{
    public class OrderTest
    {
        [Fact]
        public void SetPrice_Negative_Throws()
        {
            Assert.Throws<ArgumentException>(() => new Order(
                price: -1m,
                createdDate: DateTime.UtcNow));
        }
    }
}
