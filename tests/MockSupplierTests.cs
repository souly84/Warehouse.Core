using System;
using Xunit;

namespace Warehouse.Core.Tests
{
    public class MockSupplierTests
    {
        [Fact]
        public void FindByReceptionDate()
        {
            Assert.True(
                new MockSupplier(
                    new MockReception(DateTime.Now.AddDays(-2))
                ).Equals(DateTime.Now.AddDays(-2))
            );
        }

        [Fact]
        public void DoesNotFindByOtherReceptionDate()
        {
            Assert.False(
                new MockSupplier(
                    new MockReception(DateTime.Now.AddDays(-2))
                ).Equals(DateTime.Now.AddDays(2))
            );
        }
    }
}
