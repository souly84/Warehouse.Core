using Xunit;

namespace Warehouse.Core.Tests
{
    public class MockReceptionGoodTests
    {
        [Fact]
        public void NotEqualWhenDifferent()
        {
            Assert.NotEqual(
                new MockReceptionGood("1", 2),
                new MockReceptionGood("2", 2)
            );
        }
    }
}
