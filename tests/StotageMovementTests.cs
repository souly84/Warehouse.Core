using System.Threading.Tasks;
using MediaPrint;
using Xunit;

namespace Warehouse.Core.Tests
{
    public class StotageMovementTests
    {
        [Fact]
        public async Task MoveToIncreasesTheQuantityInTargetStorage()
        {
            var storageToMoveInto = new MockStorage();
            var good = new MockGood("1", 5);
            await good
                .From(await good.Storages.FirstAsync())
                .MoveToAsync(storageToMoveInto, 4);

            Assert.EqualJson(
                @"{
                    ""Goods"": [
                    {
                        ""Id"": ""1"",
                        ""Barcode"": null,
                        ""Quantity"": ""5"",
                        ""StorageQty"": ""4""
                    }
                  ]
                }",
                storageToMoveInto.ToJson().ToString()
            );
        }

        [Fact]
        public async Task MoveToDecreasesTheQuantityFromStorage()
        {
            
            var good = new MockGood("1", 5);
            var storageFrom = await good.Storages.FirstAsync();
            await good
                .From(storageFrom)
                .MoveToAsync(new MockStorage(), 4);

            Assert.EqualJson(
                @"{
                    ""Goods"": [
                    {
                        ""Id"": ""1"",
                        ""Barcode"": null,
                        ""Quantity"": ""5"",
                        ""StorageQty"": ""1""
                    }
                  ]
                }",
                storageFrom.ToJson().ToString()
            );
        }
    }
}
