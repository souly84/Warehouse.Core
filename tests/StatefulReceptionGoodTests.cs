using Warehouse.Core.Tests.Extensions;
using Xunit;

namespace Warehouse.Core.Tests
{
    public class StatefulReceptionGoodTests
    {
        [Fact]
        public void EqaulWhenTheSameGoods()
        {
            Assert.Equal(
                new StatefulReceptionGood(
                    new MockReceptionGood("1", 1, "1111"),
                    new KeyValueStorage("KeyStorage"),
                    "1111"
                ),
                new StatefulReceptionGood(
                    new MockReceptionGood("1", 1, "1111"),
                    new KeyValueStorage("KeyStorage"),
                    "1111"
                )
            );
        }
    }
}
