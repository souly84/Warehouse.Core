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
            var cached = new OneSuccessfulCallGoods().Cached();
            Assert.NotEmpty(await cached.ToListAsync());
            Assert.NotEmpty(await cached.ToListAsync());
        }
    }
}
