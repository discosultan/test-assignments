using Newtonsoft.Json;
using Xunit;

namespace AlbumPrinter.WebAPI.IntegrationTests.Utils
{
    // Would be nice to have these as static extensions to `Assert` class.
    static class AssertExtensions
    {
        public static void JsonEqual(object expected, object actual) =>
            Assert.Equal(
                JsonConvert.SerializeObject(expected),
                JsonConvert.SerializeObject(actual));
    }
}
