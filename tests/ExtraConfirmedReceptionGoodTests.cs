using System.Threading.Tasks;
using Warehouse.Core.Tests.Extensions;
using Xunit;

namespace Warehouse.Core.Tests
{
    public class ExtraConfirmedReceptionGoodTests
    {
        [Fact]
        public void TotalQuantity()
        {
            Assert.Equal(
                9,
                new ExtraConfirmedReceptionGood(
                    new MockReceptionGood("1", 2, "1111"),
                    new MockReceptionGood("2", 3, "2222"),
                    new MockReceptionGood("3", 4, "333")
                ).Quantity
            );
        }

        [Fact]
        public void IsExtra()
        {
            Assert.True(
                new ExtraConfirmedReceptionGood(
                    new MockReceptionGood("1", 2, "1111"),
                    new MockReceptionGood("2", 3, "2222"),
                    new MockReceptionGood("3", 4, "333")
                ).IsExtraConfirmed
            );
        }

        [Fact]
        public void IsNotUnknown()
        {
            Assert.False(
                new ExtraConfirmedReceptionGood(
                    new MockReceptionGood("1", 2, "1111"),
                    new MockReceptionGood("2", 3, "2222"),
                    new MockReceptionGood("3", 4, "333")
                ).IsUnknown
            );
        }

        // The main goal to test that GetHashCode does not throw Exception
        [Fact]
        public void HashCode()
        {
            Assert.NotEqual(
                0,
                new ExtraConfirmedReceptionGood(
                    new MockReceptionGood("1", 2, "1111"),
                    new MockReceptionGood("2", 3, "2222"),
                    new MockReceptionGood("3", 4, "333")
                ).GetHashCode()
            );
        }

        [Fact]
        public async Task FullyConfirmedOnceInitiallyConfirmed()
        {
            var initiallyExtraConfirmedGood = new ExtraConfirmedReceptionGood(
                await new MockReceptionGood("4", 3, "360602").FullyConfirmed()
            );
            Assert.True(
                await initiallyExtraConfirmedGood.ConfirmedAsync()
            );
        }

        [Fact]
        public async Task PartiallyConfirmedOnceInitiallyConfirmedWasUpdated()
        {
            var initiallyExtraConfirmedGood = new ExtraConfirmedReceptionGood(
                await new MockReceptionGood("4", 3, "360602").FullyConfirmed()
            );
            initiallyExtraConfirmedGood.Confirmation.Increase(1);
            Assert.Equal(
                IConfirmationState.ConfirmationState.Partially,
                await initiallyExtraConfirmedGood.Confirmation.State.ToEnumAsync()
            );
        }
    }
}
