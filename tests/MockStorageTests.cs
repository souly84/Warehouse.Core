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

        [Fact]
        public async Task GoodCombinesAllTheStorages()
        {
            var good = new MockWarehouseGood("1", 1, "1111");
            var storage = new MockStorage(
                "ST01",
                good.With(
                    new MockStorages(
                        new ListOfEntities<IStorage>(
                            new MockStorage("ST02", good)
                        ),
                        new ListOfEntities<IStorage>(
                            new MockStorage("ST03", good)
                        ),
                        new ListOfEntities<IStorage>(
                            new MockStorage("ST04", good)
                        )
                    )
                )
            );

            var storageGood = await storage.Goods.FirstAsync();
            Assert.Equal(
                4,
                (await storageGood.Storages.ToListAsync()).Count
            );
        }

        [Fact]
        public async Task GoodCombinesPutAwayTheStorages()
        {
            var good = new MockWarehouseGood("1", 1, "1111");
            var storage = new MockStorage(
                "ST01",
                good.With(
                    new MockStorages(
                        new ListOfEntities<IStorage>(
                            new MockStorage("ST02", good)
                        ),
                        new ListOfEntities<IStorage>(
                            new MockStorage("ST03", good)
                        ),
                        new ListOfEntities<IStorage>(
                            new MockStorage("ST04", good)
                        )
                    )
                )
            );

            var storageGood = await storage.Goods.FirstAsync();
            Assert.Equal(
                new List<IStorage> { new MockStorage("ST02", good) },
                await storageGood.Storages.PutAway.ToListAsync()
            );
        }

        [Fact]
        public async Task GoodCombinesRaceTheStorages()
        {
            var good = new MockWarehouseGood("1", 1, "1111");
            var storage = new MockStorage(
                "ST01",
                good.With(
                    new MockStorages(
                        new ListOfEntities<IStorage>(
                            new MockStorage("ST02", good)
                        ),
                        new ListOfEntities<IStorage>(
                            new MockStorage("ST03", good)
                        ),
                        new ListOfEntities<IStorage>(
                            new MockStorage("ST04", good)
                        )
                    )
                )
            );

            var storageGood = await storage.Goods.FirstAsync();
            Assert.Equal(
                new List<IStorage>
                {
                    new MockStorage("ST03", good),
                    new MockStorage("ST01", good)
                },
                await storageGood.Storages.Race.ToListAsync()
            );
        }

        [Fact]
        public async Task GoodCombinesReserveTheStorages()
        {
            var good = new MockWarehouseGood("1", 1, "1111");
            var storage = new MockStorage(
                "ST01",
                good.With(
                    new MockStorages(
                        new ListOfEntities<IStorage>(
                            new MockStorage("ST02", good)
                        ),
                        new ListOfEntities<IStorage>(
                            new MockStorage("ST03", good)
                        ),
                        new ListOfEntities<IStorage>(
                            new MockStorage("ST04", good)
                        )
                    )
                )
            );

            var storageGood = await storage.Goods.FirstAsync();

            Assert.Equal(
                new List<IStorage>
                {
                    new MockStorage("ST04", good)
                },
                await storageGood.Storages.Reserve.ToListAsync()
            );
        }
    }
}
