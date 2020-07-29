using Adnuf.Utils;
using System;
using Xunit;

namespace Adnuf.UnitTests
{
    public class IntExtensionsTest
    {
        [Theory]
        [InlineData(0, "00000000-0000-0000-0000-000000000000")]
        [InlineData(1, "00000001-0000-0000-0000-000000000000")]
        public void ToGuid_ConvertsByByteRepresentation(int input, string expectedOutput)
        {
            Assert.Equal(new Guid(expectedOutput), input.ToGuid());
        }
    }
}
