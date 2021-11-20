using System;
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
                new MockGood("1", 5).Confirmation.Decrease(1);
            });
        }

        [Fact]
        public void ThrowsInvalidOperationException_WhenTotalExceeded()
        {
            Assert.Throws<InvalidOperationException>(() =>
            {
                var good = new MockGood("1", 5);
                good.Confirmation.Increase(4);
                good.Confirmation.Increase(1);
                good.Confirmation.Increase(1);
            });
        }

        [Fact]
        public void ConfirmationDone()
        {
            Assert.True(
                new MockGood("1", 5)
                    .FullyConfirmed()
                    .Confirmed()
            );
        }
    }
}
