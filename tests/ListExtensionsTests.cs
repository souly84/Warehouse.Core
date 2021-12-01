using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Warehouse.Core.Tests
{
    public class ListExtensionsTests
    {
        [Fact]
        public async Task FirstAsync()
        {
            Assert.Equal(
                 new List<IGood>
                 {
                       new MockGood("3", 5),
                       new MockGood("4", 7)
                 },
                 await new List<IGood>
                 {
                       new MockGood("1", 1),
                       new MockGood("2", 3),
                       new MockGood("3", 5),
                       new MockGood("4", 7)
                 }.WhereAsync(good => Task.FromResult(good.Quantity > 3))
            );
        }

        [Fact]
        public async Task FirstAsync_WithPredicate()
        {
            Assert.Equal(
                 new MockGood("3", 5),
                 await new List<IGood>
                 {
                       new MockGood("1", 5),
                       new MockGood("2", 5),
                       new MockGood("3", 5),
                       new MockGood("4", 5)
                 }.FirstAsync(good => Task.FromResult(good.Equals(new MockGood("3", 5))))
            );
        }
    }
}
