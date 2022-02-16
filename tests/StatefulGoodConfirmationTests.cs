using System;
using System.Threading.Tasks;
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
                new MockReceptionGood("1", 1, "1111")
                    .Stateful(new KeyValueStorage())
                    .Confirmation.Equals(
                        new GoodConfirmation(
                            new MockReceptionGood("1", 1, "1111"),
                            1
                        )
                    )
            );
        }

        [Fact]
        public void GetHashCodeNotZero()
        {
            Assert.NotEqual(
                0,
                new MockReceptionGood("1", 1, "1111")
                    .Stateful(new KeyValueStorage())
                    .Confirmation.GetHashCode()
            );
        }

        [Fact]
        public void ThrowsInvalidOperationException_WhenDecreasedToNegative()
        {
            Assert.Throws<InvalidOperationException>(() =>
            {
                // Originally 0 Confirmed
                var confrimation = new MockReceptionGood("1", 1, "1111")
                    .Stateful(new KeyValueStorage())
                     .Confirmation.Decrease(1);
            });
        }

        [Fact]
        public void ThrowsInvalidOperationException_WhenTotalExceeded()
        {
            Assert.Throws<InvalidOperationException>(() =>
            {
                var confrimation = new MockReceptionGood("1", 5, "1111")
                    .Stateful(new KeyValueStorage())
                    .Confirmation;
                confrimation.Increase(4);
                confrimation.Increase(1);
                confrimation.Increase(1);
            });
        }

        [Fact]
        public async Task ConfirmationDone()
        {
            var confirmedGood = await new MockReceptionGood("1", 1, "1111")
                .Stateful(new KeyValueStorage())
                .Confirmation
                .FullyConfirmed();
            Assert.True(
               await confirmedGood.Good.ConfirmedAsync()
            );
        }

        [Fact]
        public async Task AlreadyConfirmed()
        {
            var confirmedGood = await new MockReceptionGood("1", 5, "1111").FullyConfirmed();
            Assert.True(
                await confirmedGood
                    .Stateful(new KeyValueStorage())
                    .Confirmation
                    .DoneAsync()
            );
        }

        [Fact]
        public async Task ConfirmationClear()
        {
            var confirmation = await new MockReceptionGood("1", 1, "1111")
                .Stateful(new KeyValueStorage())
                .Confirmation
                .FullyConfirmed();
            var notConfirmed = confirmation.Good.Clear();
            Assert.False(
                await notConfirmed.ConfirmedAsync()
            );
        }
    }
}
