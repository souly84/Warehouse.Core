using System.Threading.Tasks;
using Xunit;

namespace Warehouse.Core.Tests
{
    public class EntitiesExtensionsTests
    {
        [Fact]
        public async Task FirstAsync()
        {
            Assert.Equal(
                 new MockGood("1", 5),
                 await new ListOfEntities<IGood>(
                       new MockGood("1", 5),
                       new MockGood("2", 5),
                       new MockGood("3", 5),
                       new MockGood("4", 5)
                 ).FirstAsync()
            );
        }

        [Fact]
        public async Task FirstAsync_WithPredicate()
        {
            Assert.Equal(
                 new MockGood("3", 5),
                 await new ListOfEntities<IGood>(
                       new MockGood("1", 5),
                       new MockGood("2", 5),
                       new MockGood("3", 5),
                       new MockGood("4", 5)
                 ).FirstAsync(good => good.Equals(new MockGood("3", 5)))
            );
        }
    }
}
