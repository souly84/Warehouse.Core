using System;
using System.Threading.Tasks;
using MediaPrint;
using Xunit;

namespace Warehouse.Core.Tests
{
    public class StockMovementTests
    {
        [Fact]
        public async Task MoveToIncreasesTheQuantityInTargetStorage()
        {
            var storageToMoveInto = new MockStorage();
            var good = new MockWarehouseGood("1", 5);
            await good
                .From(await good.Storages.FirstAsync())
                .MoveToAsync(storageToMoveInto, 4);

            Assert.EqualJson(
                @"{
                    ""Goods"": [
                    {
                        ""Id"": ""1"",
                        ""Barcode"": null,
                        ""Quantity"": ""4""
                    }
                  ]
                }",
                storageToMoveInto.ToJson().ToString()
            );
        }

        [Fact]
        public async Task MoveToDecreasesTheQuantityFromStorage()
        {
            
            var good = new MockWarehouseGood("1", 5);
            var storageFrom = await good.Storages.Reserve.FirstAsync();
            await good
                .From(storageFrom)
                .MoveToAsync(new MockStorage(), 4);

            Assert.EqualJson(
                @"{
                    ""Goods"": [
                    {
                        ""Id"": ""1"",
                        ""Barcode"": null,
                        ""Quantity"": ""1""
                    }
                  ]
                }",
                storageFrom.ToJson().ToString()
            );
        }

        [Fact]
        public Task StockMoveRaiseOperationExceptionWhenNoFromStorageBeingTargeted()
        {
            return Assert.ThrowsAsync<InvalidOperationException>(() =>
                new MockWarehouseGood("1", 5)
                    .Movement
                    .MoveToAsync(new MockStorage(), 4)
            );
        }
    }
}
