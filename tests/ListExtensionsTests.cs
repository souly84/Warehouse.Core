using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Warehouse.Core.Tests
{
    public class ListExtensionsTests
    {
        [Fact]
        public async Task WhereAsync()
        {
            Assert.Equal(
                 new List<IReceptionGood>
                 {
                       new MockReceptionGood("3", 5),
                       new MockReceptionGood("4", 7)
                 },
                 await new List<IReceptionGood>
                 {
                       new MockReceptionGood("1", 1),
                       new MockReceptionGood("2", 3),
                       new MockReceptionGood("3", 5),
                       new MockReceptionGood("4", 7)
                 }.WhereAsync(good => Task.FromResult(good.Quantity > 3))
            );
        }

        [Fact]
        public Task ItemNotFoundException()
        {
            return Assert.ThrowsAsync<InvalidOperationException>(() =>
                 new List<IReceptionGood>
                 {
                       new MockReceptionGood("1", 1),
                       new MockReceptionGood("2", 3),
                       new MockReceptionGood("3", 5),
                       new MockReceptionGood("4", 7)
                 }.FirstAsync(a => Task.FromResult(a.Equals("NotExistingId")))
            );
        }

        [Fact]
        public async Task FirstAsync_WithPredicate()
        {
            Assert.Equal(
                 new MockReceptionGood("3", 5),
                 await new List<IReceptionGood>
                 {
                       new MockReceptionGood("1", 5),
                       new MockReceptionGood("2", 5),
                       new MockReceptionGood("3", 5),
                       new MockReceptionGood("4", 5)
                 }.FirstAsync(good => Task.FromResult(good.Equals(new MockReceptionGood("3", 5))))
            );
        }

        [Fact]
        public async Task FirstOrDefaultAsync_WithPredicate()
        {
            Assert.Equal(
                 new MockReceptionGood("3", 5),
                 await new List<IReceptionGood>
                 {
                       new MockReceptionGood("1", 5),
                       new MockReceptionGood("2", 5),
                       new MockReceptionGood("3", 5),
                       new MockReceptionGood("4", 5)
                 }.FirstOrDefaultAsync(good => Task.FromResult(good.Equals(new MockReceptionGood("3", 5))))
            );
        }

        [Fact]
        public async Task NullWhenNotFound()
        {
            Assert.Null(
                 await new List<IReceptionGood>
                 {
                       new MockReceptionGood("1", 1),
                       new MockReceptionGood("2", 3),
                       new MockReceptionGood("3", 5),
                       new MockReceptionGood("4", 7)
                 }.FirstOrDefaultAsync(a => Task.FromResult(a == null))
            );
        }
    }
}
