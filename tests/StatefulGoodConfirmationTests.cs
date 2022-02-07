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
                new StatefulGoodConfirmation(
                    new GoodConfirmation(new MockReceptionGood("1", 1, "1111"), 0),
                    new KeyValueStorage(),
                    "11111"
                ).Equals(new GoodConfirmation(new MockReceptionGood("1", 1, "1111"), 0))
            );
        }

        [Fact]
        public void GetHashCodeNotZero()
        {
            Assert.NotEqual(
                0,
                new StatefulGoodConfirmation(
                    new GoodConfirmation(new MockReceptionGood("1", 1, "1111"), 0),
                    new KeyValueStorage(),
                    "11111"
                ).GetHashCode()
            );
        }

        [Fact]
        public void ThrowsInvalidOperationException_WhenDecreasedToNegative()
        {
            Assert.Throws<InvalidOperationException>(() =>
            {
                new StatefulGoodConfirmation(
                    new MockReceptionGood("1", 5).Confirmation,
                    new KeyValueStorage(),
                    "11111"
                ).Decrease(1);
            });
        }

        [Fact]
        public void ThrowsInvalidOperationException_WhenTotalExceeded()
        {
            Assert.Throws<InvalidOperationException>(() =>
            {
                var confrimation = new StatefulGoodConfirmation(
                    new MockReceptionGood("1", 5).Confirmation,
                    new KeyValueStorage(),
                    "11111"
                );
                confrimation.Increase(4);
                confrimation.Increase(1);
                confrimation.Increase(1);
            });
        }

        [Fact]
        public async Task ConfirmationDone()
        {
            var confrimation = new StatefulGoodConfirmation(
                new MockReceptionGood("1", 5).Confirmation,
                new KeyValueStorage(),
                "11111"
            );
            var confirmedGood = await confrimation.FullyConfirmed();
            Assert.True(
               await confirmedGood.Good.ConfirmedAsync()
            );
        }

        [Fact]
        public async Task AlreadyConfirmed()
        {
            Assert.True(
                await new StatefulGoodConfirmation(
                    new GoodConfirmation(
                       new MockReceptionGood("1", 5),
                       5,
                       5
                    ),
                    new KeyValueStorage(),
                    "11111"
                ).DoneAsync()
            );
        }

        [Fact]
        public async Task ConfirmationClear()
        {
            var confrimation = new StatefulGoodConfirmation(
                new MockReceptionGood("1", 5).Confirmation,
                new KeyValueStorage(),
                "11111"
            );
            var confirmedGood = await confrimation.FullyConfirmed();
            var notConfirmed = confirmedGood.Good.Clear();
            Assert.False(
                await notConfirmed.ConfirmedAsync()
            );
        }
    }
}
