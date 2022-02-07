using Warehouse.Core.Tests.Extensions;
using Xunit;

namespace Warehouse.Core.Tests
{
    public class StatefulGoodConfirmationTests
    {
        [Fact]
        public void EqualsToOriginalConfirmation()
        {
            Assert.True(
                new StatefulGoodConfirmation(
                    new GoodConfirmation(new MockReceptionGood("1", 1, "1111"), 0),
                    new KeyValueStorage("EqualsToOriginalConfirmation"),
                    "11111"
                ).Equals(new GoodConfirmation(new MockReceptionGood("1", 1, "1111"), 0))
            );
        }
    }
}
