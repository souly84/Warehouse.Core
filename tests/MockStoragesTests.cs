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
                    new ListOfEntities<IStorage>(new MockStorage(), new MockStorage()),
                    new ListOfEntities<IStorage>(new MockStorage(), new MockStorage())
                ).InLocalFirst()
                 .ByBarcodeAsync("4567890")
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
                    new ListOfEntities<IStorage>(new MockStorage(), new MockStorage()),
                    new ListOfEntities<IStorage>(new MockStorage(), new MockStorage())
                ).InLocalFirst()
                 .ByBarcodeAsync("4567890")
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
                    new ListOfEntities<IStorage>(new MockStorage("4567890")),
                    new ListOfEntities<IStorage>(new MockStorage(), new MockStorage())
                ).InLocalFirst()
                 .ByBarcodeAsync("4567890")
            );
        }

        [Fact]
        public async Task ByBarcodeInRemote()
        {
            Assert.Equal(
                new MockStorage("4567890"),
                await new MockStorages(
                    new ListOfEntities<IStorage>(new MockStorage(), new MockStorage()),
                    new ListOfEntities<IStorage>(new MockStorage(), new MockStorage()),
                    new ListOfEntities<IStorage>(new MockStorage()),
                    new ListOfEntities<IStorage>(new MockStorage(), new MockStorage("4567890"))
                ).InLocalFirst()
                 .ByBarcodeAsync("4567890")
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
                    new ListOfEntities<IStorage>(new MockStorage("4567890")),
                    new ListOfEntities<IStorage>(new MockStorage(), new MockStorage())
                ).InLocalFirst()
                 .ByBarcodeAsync("543212")
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
                    new ListOfEntities<IStorage>(new MockStorage("4567890")),
                    new ListOfEntities<IStorage>(new MockStorage(), new MockStorage())
                ).InLocalFirst()
                 .ToListAsync())
                 .Count
            );
        }

        [Fact]
        public void InLocalFirst_WithFilter()
        {
            Assert.IsType<InLocalFirstStorages>(
               new MockStorages(
                    new ListOfEntities<IStorage>(new MockStorage(), new MockStorage()),
                    new ListOfEntities<IStorage>(new MockStorage(), new MockStorage()),
                    new ListOfEntities<IStorage>(new MockStorage("4567890")),
                    new ListOfEntities<IStorage>(new MockStorage(), new MockStorage())
                ).InLocalFirst()
                 .With(new EmptyFilter())
            );
        }
    }
}
