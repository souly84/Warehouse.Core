using System;
using System.Threading.Tasks;
using Warehouse.Core.Tests.Extensions;
using Xunit;

namespace Warehouse.Core.Tests
{
    public class GoodConfirmationTests
    {
        [Fact]
        public void ThrowsInvalidOperationException_WhenDecreasedToNegative()
        {
            Assert.Throws<InvalidOperationException>(() =>
            {
                new MockReceptionGood("1", 5).Confirmation.Decrease(1);
            });
        }

        [Fact]
        public void ThrowsInvalidOperationException_WhenTotalExceeded()
        {
            Assert.Throws<InvalidOperationException>(() =>
            {
                var good = new MockReceptionGood("1", 5);
                good.Confirmation.Increase(4);
                good.Confirmation.Increase(1);
                good.Confirmation.Increase(1);
            });
        }

        [Fact]
        public async Task ConfirmationDone()
        {
            var confirmedGood = await new MockReceptionGood("1", 5).FullyConfirmed();
            Assert.True(
               await confirmedGood.ConfirmedAsync()
            );
        }

        [Fact]
        public async Task AlreadyConfirmed()
        {
            Assert.True(
               await new GoodConfirmation(
                   new MockReceptionGood("1", 5),
                   5,
                   5
               ).DoneAsync()
            );
        }

        [Fact]
        public async Task AlreadyExtraConfirmed()
        {
            Assert.True(
               await new GoodConfirmation(
                   new MockReceptionGood("1", 5),
                   5,
                   6
               ).DoneAsync()
            );
        }

        [Fact]
        public async Task ConfirmationClear()
        {
            var confirmedGood = await new MockReceptionGood("1", 5).FullyConfirmed();
            var notConfirmed = confirmedGood.Clear();
            Assert.False(
                await notConfirmed.ConfirmedAsync()
            );
        }

        [Fact]
        public async Task EqualToTheSame()
        {
            Assert.Equal(
               (await new MockReceptionGood("1", 4, "360601").FullyConfirmed()).Confirmation,
               (await new MockReceptionGood("1", 4, "360601").FullyConfirmed()).Confirmation
            );
        }
    }
}
