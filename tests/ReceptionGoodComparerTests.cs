using Xunit;

namespace Warehouse.Core.Tests
{
    public class ReceptionGoodComparerTests
    {
        [Fact]
        public void GoodConfirmationNullEqualToNull()
        {
            Assert.Equal(
                0,
                new ReceptionGoodComparer().Compare((IGoodConfirmation)null, (IGoodConfirmation)null)
            );
        }

        [Fact]
        public void ReceptionGoodNullEqualToNull()
        {
            Assert.Equal(
                0,
                new ReceptionGoodComparer().Compare((IReceptionGood)null, (IReceptionGood)null)
            );
        }

        [Fact]
        public void NotNullIsGreaterThanNull()
        {
            Assert.Equal(
                1,
                new ReceptionGoodComparer().Compare(
                    new GoodConfirmation(new MockReceptionGood("1", 1), 1),
                    null
                )
            );
        }

        [Fact]
        public void NullIsLessThanNonNull()
        {
            Assert.Equal(
                -1,
                new ReceptionGoodComparer().Compare(
                    null,
                    new GoodConfirmation(new MockReceptionGood("1", 1), 1)
                )
            );
        }
    }
}
