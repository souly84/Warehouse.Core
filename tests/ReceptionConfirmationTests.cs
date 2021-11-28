using System.Threading.Tasks;
using MediaPrint;
using Warehouse.Core.Receptions;
using Warehouse.Core.Tests.Extensions;
using Xunit;

namespace Warehouse.Core.Tests
{
    public class ReceptionConfirmationTests
    {
        [Fact]
        public async Task Reception_FullConfirmation()
        {
            var reception = await new ConfirmedReception<MockReception>(
                new MockReception(
                    new MockGood("good1", 4),
                    new MockGood("good2", 8)
                )
            ).ConfirmAsync();
            Assert.True(
                 await reception.ConfirmedAsync()
            );
            Assert.Equal(
                await new ConfirmedGoods(
                    new MockGood("good1", 4),
                    new MockGood("good2", 8)
                ).ToListAsync(),
                reception.ValidatedGoods
            );  
        }

        [Fact]
        public async Task Reception_Clear()
        {
            var reception = await new ConfirmedReception<MockReception>(
                new MockReception(
                    new MockGood("good1", 4),
                    new MockGood("good2", 8)
                )
            ).ConfirmAsync();
            await reception.Confirmation().ClearAsync();
            Assert.False(
                await reception.ConfirmedAsync()
            );
        }

        [Fact]
        public async Task Reception_Confirmation_AddByGood()
        {
            var goodToConfirm = new MockGood("good1", 4);
            await new MockReception(
                goodToConfirm,
                new MockGood("good2", 8)
            ).Confirmation().AddAsync(goodToConfirm);
            Assert.EqualJson(
                @"{
                    ""Good"":
                    {
                        ""Id"": ""good1"",
                        ""Barcode"": null
                    },
                    ""Total"": ""4"",
                    ""Confirmed"": ""1"",
                    ""State"": ""Partially""
                }",
                goodToConfirm.Confirmation.ToJson().ToString()
            );
        }

        [Fact]
        public async Task Reception_Confirmation_RemoveByGood()
        {
            var goodToConfirm = new MockGood("good1", 4);
            var confirmation = new MockReception(
                goodToConfirm,
                new MockGood("good2", 8)
            ).Confirmation();
            await confirmation.AddAsync(goodToConfirm);
            await confirmation.RemoveAsync(goodToConfirm);
            Assert.EqualJson(
                @"{
                    ""Good"":
                    {
                        ""Id"": ""good1"",
                        ""Barcode"": null
                    },
                    ""Total"": ""4"",
                    ""Confirmed"": ""0"",
                    ""State"": ""NotStarted""
                }",
                goodToConfirm.Confirmation.ToJson().ToString()
            );
        }

        [Fact]
        public async Task Reception_Confirmation_AddByGoodBarcode()
        {
            var goodToConfirm = new MockGood("good1", 4, "360600");
            await new MockReception(
                goodToConfirm,
                new MockGood("good2", 8)
            ).Confirmation().AddAsync("360600");
            Assert.EqualJson(
                @"{
                    ""Good"":
                    {
                        ""Id"": ""good1"",
                        ""Barcode"": ""360600""
                    },
                    ""Total"": ""4"",
                    ""Confirmed"": ""1"",
                    ""State"": ""Partially""
                }",
                goodToConfirm.Confirmation.ToJson().ToString()
            );
        }

        [Fact]
        public async Task Reception_Confirmation_RemoveByGoodBarcode()
        {
            var goodToConfirm = new MockGood("good1", 4, "360600");
            var confirmation = new MockReception(
                goodToConfirm,
                new MockGood("good2", 8)
            ).Confirmation();
            await confirmation.AddAsync(goodToConfirm);
            await confirmation.RemoveAsync("360600");
            Assert.EqualJson(
                @"{
                    ""Good"":
                    {
                        ""Id"": ""good1"",
                        ""Barcode"": ""360600""
                    },
                    ""Total"": ""4"",
                    ""Confirmed"": ""0"",
                    ""State"": ""NotStarted""
                }",
                goodToConfirm.Confirmation.ToJson().ToString()
            );
        }

        [Fact]
        public async Task Reception_Confirmation_ExistsByGoodBarcode()
        {
            Assert.True(
                await new MockReception(
                    new MockGood("good1", 4, "360600"),
                    new MockGood("good2", 8)
                ).Confirmation()
                 .ExistsAsync("360600")
            );
        }

        [Fact]
        public async Task Reception_Confirmation_DoesNotExistByGoodBarcode()
        {
            Assert.False(
                await new MockReception(
                    new MockGood("good1", 4, "360601"),
                    new MockGood("good2", 8, "360602")
                ).Confirmation()
                 .ExistsAsync("360600")
            );
        }

        [Fact]
        public async Task HistoryShowsFullyConfimedGoodsOnly()
        {
            Assert.Contains(
                (await new MockGood("good1", 4, "360601").FullyConfirmed()).Confirmation,
                await new MockReception(
                    await new MockGood("good1", 4, "360601").FullyConfirmed(),
                    await new MockGood("good2", 8, "360602").PartiallyConfirmed(4)
                ).Confirmation()
                 .History()
                 .ToListAsync()
            );
        }

        [Fact]
        public async Task NotConfirmedOnly_ShowsNotConfirmedGoodsConfirmations()
        {
            Assert.Contains(
                (await new MockGood("good2", 8, "360602").PartiallyConfirmed(4)).Confirmation,
                await new MockReception(
                    await new MockGood("good1", 4, "360601").FullyConfirmed(),
                    await new MockGood("good2", 8, "360602").PartiallyConfirmed(4)
                ).NeedConfirmation()
                 .ToListAsync()
            );
        }
    }
}
