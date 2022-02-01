using System;
using System.Threading.Tasks;
using Xunit;

namespace Warehouse.Core.Tests
{
    public class MockWarehouseTests
    {
        [Fact]
        public async Task StoragesGoodsAreCombined()
        {
            Assert.Equal(
                6,
                (await new MockWarehouse(
                    new MockStorage(
                        new MockWarehouseGood("1", 4),
                        new MockWarehouseGood("2", 4)
                    ),
                    new MockStorage(
                        new MockWarehouseGood("1", 4),
                        new MockWarehouseGood("3", 4)
                    ),
                    new MockStorage(
                        new MockWarehouseGood("1", 4),
                        new MockWarehouseGood("4", 4)
                    )
                ).Goods.ToListAsync())
                 .Count
            );
        }
    }
}
