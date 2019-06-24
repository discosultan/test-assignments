using System;
using Xunit;

namespace AlbumPrinter.UnitTests
{
    public class CustomerTest
    {
        // We could combine setter tests into a theory for significantly reduced
        // boilerplate.

        [Fact]
        public void SetName_Null_Throws()
        {
            Assert.Throws<ArgumentNullException>(() => new Customer(
                name: null,
                email: "dummy"));
        }

        [Fact]
        public void SetEmail_Null_Throws()
        {
            Assert.Throws<ArgumentNullException>(() => new Customer(
                name: "dummy",
                email: null));
        }
    }
}
