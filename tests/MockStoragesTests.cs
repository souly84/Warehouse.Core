using System;
using System.Threading.Tasks;
using Xunit;

namespace Warehouse.Core.Tests
{
    public class MockStoragesTests
    {
        [Fact]
        public async Task ByBarcodeInPutAway()
        {
            Assert.Equal(
                new MockStorage("4567890"),
                await new MockStorages(
                    new ListOfEntities<IStorage>(new MockStorage("4567890")),
                    new ListOfEntities<IStorage>(new MockStorage(), new MockStorage()),
                    new ListOfEntities<IStorage>(new MockStorage(), new MockStorage())
                ).ByBarcodeAsync("4567890")
            );
        }

        [Fact]
        public async Task GoodQuantityByWarehouseGood()
        {
            Assert.Equal(
                2,
                await new MockStorage(
                    new MockWarehouseGood("1", 2, "1111")
                ).QuantityForAsync(new MockWarehouseGood("1", 2, "1111"))
            );
        }

        [Fact]
        public async Task GoodQuantityByBarcode()
        {
            Assert.Equal(
                2,
                await new MockStorage(
                    new MockWarehouseGood("1", 2, "1111")
                ).QuantityForAsync("1111")
            );
        }

        [Fact]
        public async Task ZeroQuantityForNonExistingGood()
        {
            Assert.Equal(
                0,
                await new MockStorage(
                    new MockWarehouseGood("1", 2, "1111")
                ).QuantityForAsync(new MockWarehouseGood("2", 2, "2222"))
            );
        }

        [Fact]
        public async Task ByBarcodeInRace()
        {
            Assert.Equal(
                new MockStorage("4567890"),
                await new MockStorages(
                    new ListOfEntities<IStorage>(new MockStorage(), new MockStorage()),
                    new ListOfEntities<IStorage>(new MockStorage("4567890")),
                    new ListOfEntities<IStorage>(new MockStorage(), new MockStorage())
                ).ByBarcodeAsync("4567890")
            );
        }

        [Fact]
        public async Task ByBarcodeInReserve()
        {
            Assert.Equal(
                new MockStorage("4567890"),
                await new MockStorages(
                    new ListOfEntities<IStorage>(new MockStorage(), new MockStorage()),
                    new ListOfEntities<IStorage>(new MockStorage(), new MockStorage()),
                    new ListOfEntities<IStorage>(new MockStorage("4567890"))
                ).ByBarcodeAsync("4567890")
            );
        }

        [Fact]
        public async Task ByBarcodeInWarehouse()
        {
            Assert.Equal(
                new MockStorage("4567890"),
                await new MockStorages(
                    new ListOfEntities<IStorage>(new MockStorage(), new MockStorage()),
                    new ListOfEntities<IStorage>(new MockStorage(), new MockStorage()),
                    new ListOfEntities<IStorage>(new MockStorage())
                ).ByBarcodeAsync(
                    new MockWarehouse(
                        new ListOfEntities<IWarehouseGood>(),
                        new ListOfEntities<IStorage>(new MockStorage("112121212"), new MockStorage("4567890"))
                    ),
                    "4567890")
            );
        }

        [Fact]
        public Task ByBarcodeNotExistingBarcode()
        {
            return Assert.ThrowsAsync<InvalidOperationException>(
                () => 
                 new MockStorages(
                    new ListOfEntities<IStorage>(new MockStorage(), new MockStorage()),
                    new ListOfEntities<IStorage>(new MockStorage(), new MockStorage()),
                    new ListOfEntities<IStorage>(new MockStorage("4567890"))
                ).ByBarcodeAsync(
                    new MockWarehouse(
                        new ListOfEntities<IWarehouseGood>(),
                        new ListOfEntities<IStorage>(new MockStorage("112121212"), new MockStorage("4567890"))
                    ),
                    "543212"
                )
            );
        }

        [Fact]
        public async Task ToList_ReturnsAllFromLocalCollections()
        {
            Assert.Equal(
                5,
                (await new MockStorages(
                    new ListOfEntities<IStorage>(new MockStorage(), new MockStorage()),
                    new ListOfEntities<IStorage>(new MockStorage(), new MockStorage()),
                    new ListOfEntities<IStorage>(new MockStorage("4567890"))
                ).ToListAsync())
                 .Count
            );
        }

        [Fact]
        public void WithFilter()
        {
            Assert.IsType<MockStorages>(
               new MockStorages(
                    new ListOfEntities<IStorage>(new MockStorage(), new MockStorage()),
                    new ListOfEntities<IStorage>(new MockStorage(), new MockStorage()),
                    new ListOfEntities<IStorage>(new MockStorage("4567890"))
                ).With(new EmptyFilter())
            );
        }
    }
}
