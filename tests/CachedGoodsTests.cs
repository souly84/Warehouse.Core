using System.Threading.Tasks;
using Warehouse.Core.Tests.Extensions;
using Xunit;

namespace Warehouse.Core.Tests
{
    public class CachedGoodsTests
    {
        [Fact]
        public async Task CachesToListAsync()
        {
            var cached = new OnSuccesfulCallEntities<IReceptionGood>(
                new MockReceptionGood("1", 1),
                new MockReceptionGood("2", 1)
            ).Cached();
            Assert.NotEmpty(await cached.ToListAsync());
            Assert.NotEmpty(await cached.ToListAsync());
        }
    }
}
