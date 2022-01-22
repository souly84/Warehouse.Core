using System.Collections.Generic;
using System.Threading.Tasks;
using MediaPrint;
using Xunit;

namespace Warehouse.Core.Tests
{
    public class EntitiesExtensionsTests
    {
        [Fact]
        public async Task FirstAsync()
        {
            Assert.Equal(
                 new MockReceptionGood(1, 5),
                 await new ListOfEntities<IReceptionGood>(
                       new MockReceptionGood(1, 5),
                       new MockReceptionGood(2, 5),
                       new MockReceptionGood(3, 5),
                       new MockReceptionGood(4, 5)
                 ).FirstAsync()
            );
        }

        [Fact]
        public async Task FirstAsync_WithPredicate()
        {
            Assert.Equal(
                 new MockReceptionGood(3, 5),
                 await new ListOfEntities<IReceptionGood>(
                       new MockReceptionGood(1, 5),
                       new MockReceptionGood(2, 5),
                       new MockReceptionGood(3, 5),
                       new MockReceptionGood(4, 5)
                 ).FirstAsync(good => good.Equals(new MockReceptionGood(3, 5)))
            );
        }

        [Fact]
        public async Task FirstOrDefaultAsync_ReturnFirstWhenFound()
        {
            Assert.Equal(
                 new MockReceptionGood(1, 5),
                 await new ListOfEntities<IReceptionGood>(
                       new MockReceptionGood(1, 5),
                       new MockReceptionGood(2, 5),
                       new MockReceptionGood(3, 5),
                       new MockReceptionGood(4, 5)
                 ).FirstOrDefaultAsync((good) => good.Equals(new MockReceptionGood(1, 5)))
            );
        }

        [Fact]
        public async Task FirstOrDefaultAsync_ReturnDefaultWhenNotFound()
        {
            Assert.Null(
                 await new ListOfEntities<IReceptionGood>(
                       new MockReceptionGood(1, 5),
                       new MockReceptionGood(2, 5),
                       new MockReceptionGood(3, 5),
                       new MockReceptionGood(4, 5)
                 ).FirstOrDefaultAsync((good) => good.Equals(new MockReceptionGood(222, 5)))
            );
        }

        [Fact]
        public async Task SelectAsync()
        {
            Assert.Equal(
                new List<IWarehouseGood> { new MockWarehouseGood("1", 5) },
                await new ListOfEntities<IReceptionGood>(
                    new MockReceptionGood(1, 5)
                ).SelectAsync(x =>
                    new MockWarehouseGood(x.ToDictionary().Value<string>("id"), x.Quantity)
                )
            );
        }
    }
}
