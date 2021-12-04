using System.Threading.Tasks;
using Warehouse.Core.Receptions.Goods;
using Xunit;

namespace Warehouse.Core.Tests
{
    public class EntitiesExtensionsTests
    {
        [Fact]
        public async Task FirstAsync()
        {
            Assert.Equal(
                 new MockReceptionGood("1", 5),
                 await new ListOfEntities<IReceptionGood>(
                       new MockReceptionGood("1", 5),
                       new MockReceptionGood("2", 5),
                       new MockReceptionGood("3", 5),
                       new MockReceptionGood("4", 5)
                 ).FirstAsync()
            );
        }

        [Fact]
        public async Task FirstAsync_WithPredicate()
        {
            Assert.Equal(
                 new MockReceptionGood("3", 5),
                 await new ListOfEntities<IReceptionGood>(
                       new MockReceptionGood("1", 5),
                       new MockReceptionGood("2", 5),
                       new MockReceptionGood("3", 5),
                       new MockReceptionGood("4", 5)
                 ).FirstAsync(good => good.Equals(new MockReceptionGood("3", 5)))
            );
        }
    }
}
