using Xunit;

namespace Warehouse.Core.Tests
{
    public class MockReceptionTests
    {
        [Fact]
        public void MockReceptionGetHashCode()
        {
            Assert.NotEqual(
                0,
                new MockReception(new MockReceptionGood("1", 1)).GetHashCode()
            );
        }

        [Fact]
        public void EqualsTheSame()
        {
            var reception = new MockReception(new MockReceptionGood("1", 1));
            Assert.True(
                reception.Equals(reception)
            );
        }
    }
}
