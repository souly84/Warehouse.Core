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
        public Task ByBarcodeNotExistingBarcode()
        {
            return Assert.ThrowsAsync<InvalidOperationException>(
                () => 
                 new MockStorages(
                    new ListOfEntities<IStorage>(new MockStorage(), new MockStorage()),
                    new ListOfEntities<IStorage>(new MockStorage(), new MockStorage()),
                    new ListOfEntities<IStorage>(new MockStorage("4567890"))
                ).ByBarcodeAsync("543212")
            );
        }
    }
}
