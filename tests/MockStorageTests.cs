using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Warehouse.Core.Tests
{
    public class MockStorageTests
    {
        [Fact]
        public async Task RemoveGoodWhenDecreasedToZero()
        {
            var storage = new MockStorage(new MockWarehouseGood("1", 1, "1111"));
            await storage.DecreaseAsync(new MockWarehouseGood("1", 1, "1111"), 1);
            Assert.Empty(await storage.Goods.ToListAsync());
        }

        [Fact]
        public void CanBePlacedInDictionary()
        {
            var dictionary = new Dictionary<IStorage, int>();
            dictionary.Add(new MockStorage(new MockWarehouseGood("1", 1, "1111")), 1);
            Assert.Equal(
                1,
                dictionary[new MockStorage(new MockWarehouseGood("1", 1, "1111"))]
            );
        }

        [Fact]
        public void CanBePlacedInList()
        {
            var list = new List<IStorage>()
            {
                new MockStorage(new MockWarehouseGood("1", 1, "1111"))
            };
            Assert.Contains(
                new MockStorage(new MockWarehouseGood("1", 1, "1111")),
                list
            );
        }
    }
}
