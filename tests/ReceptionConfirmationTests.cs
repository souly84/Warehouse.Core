using System.Collections.Generic;
using System.Threading.Tasks;
using MediaPrint;
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
                (MockReception) await new MockSupplier(
                    new MockReception(
                        "1",
                        new MockReceptionGood("1", 4),
                        new MockReceptionGood("2", 8)
                    )
                ).Receptions.FirstAsync()
            ).ConfirmAsync();
            Assert.True(
                 await reception.ConfirmedAsync()
            );
            Assert.Equal(
                await new ConfirmedGoods(
                    new MockReceptionGood("1", 4),
                    new MockReceptionGood("2", 8)
                ).ToListAsync(),
                reception.ValidatedGoods
            );  
        }

        [Fact]
        public async Task Reception_Validation_ForStartedConfirmationsOnly()
        {
            var reception = new MockReception(
                "1",
                new MockReceptionGood("1", 4),
                new MockReceptionGood("2", 8),
                new MockReceptionGood("3", 3)
            );

            await reception.Confirmation().AddAsync(new MockReceptionGood("1", 4), 4);
            await reception.Confirmation().AddAsync(new MockReceptionGood("3", 3), 2);
            await reception.Confirmation().CommitAsync();
            Assert.Equal(
                new List<IGoodConfirmation>
                {
                    (await new MockReceptionGood("1", 4).FullyConfirmed()).Confirmation,
                    (await new MockReceptionGood("3", 3).PartiallyConfirmed(2)).Confirmation
                },
                reception.ValidatedGoods
            );
        }

        [Fact]
        public async Task Reception_ClearConfirmation()
        {
            var reception = await new ConfirmedReception<MockReception>(
                (MockReception)await new MockSupplier()
                    .Receptions
                    .FirstAsync()
            ).ConfirmAsync();
            await reception.Confirmation().ClearAsync();
            Assert.False(
                await reception.ConfirmedAsync()
            );
        }

        [Fact]
        public async Task Reception_Confirmation_ByGood()
        {
            var goodToConfirm = new MockReceptionGood("1", 4);
            await new MockReception(
                "1",
                goodToConfirm,
                new MockReceptionGood("2", 8)
            ).Confirmation().AddAsync(goodToConfirm);
            Assert.EqualJson(
                @"{
                    ""Good"":
                    {
                        ""Id"": ""1"",
                        ""Barcode"": null,
                        ""IsUnknown"": false,
                        ""IsExtraConfirmed"": false,
                        ""Quantity"": ""4""
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
            var goodToConfirm = new MockReceptionGood("1", 4);
            var confirmation = new MockReception(
                "1",
                goodToConfirm,
                new MockReceptionGood("2", 8)
            ).Confirmation();
            await confirmation.AddAsync(goodToConfirm);
            await confirmation.RemoveAsync(goodToConfirm);
            Assert.EqualJson(
                @"{
                    ""Good"":
                    {
                        ""Id"": ""1"",
                        ""Barcode"": null,
                        ""IsUnknown"": false,
                        ""IsExtraConfirmed"": false,
                        ""Quantity"": ""4""
                    },
                    ""Total"": ""4"",
                    ""Confirmed"": ""0"",
                    ""State"": ""NotStarted""
                }",
                goodToConfirm.Confirmation.ToJson().ToString()
            );
        }

        [Fact]
        public async Task Reception_Confirmation_ByGoodBarcode()
        {
            var goodToConfirm = new MockReceptionGood("1", 4, "360600");
            await new MockReception(
                "1",
                goodToConfirm,
                new MockReceptionGood("2", 8)
            ).Confirmation().AddAsync("360600");
            Assert.EqualJson(
                @"{
                    ""Good"":
                    {
                        ""Id"": ""1"",
                        ""Barcode"": ""360600"",
                        ""IsUnknown"": false,
                        ""IsExtraConfirmed"": false,
                        ""Quantity"": ""4""
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
            var goodToConfirm = new MockReceptionGood("1", 4, "360600");
            var confirmation = new MockReception(
                "1",
                goodToConfirm,
                new MockReceptionGood("2", 8)
            ).Confirmation();
            await confirmation.AddAsync(goodToConfirm);
            await confirmation.RemoveAsync("360600");
            Assert.EqualJson(
                @"{
                    ""Good"":
                    {
                        ""Id"": ""1"",
                        ""Barcode"": ""360600"",
                        ""IsUnknown"": false,
                        ""IsExtraConfirmed"": false,
                        ""Quantity"": ""4""
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
                    "1",
                    new MockReceptionGood("1", 4, "360600"),
                    new MockReceptionGood("2", 8)
                ).Confirmation()
                 .ExistsAsync("360600")
            );
        }

        [Fact]
        public async Task Reception_Confirmation_DoesNotExistByGoodBarcode()
        {
            Assert.False(
                await new MockReception(
                    "1",
                    new MockReceptionGood("1", 4, "360601"),
                    new MockReceptionGood("2", 8, "360602")
                ).Confirmation()
                 .ExistsAsync("360600")
            );
        }

        [Fact]
        // Souleymen, should show fully confirmed or partially confirmed should be presented also???
        public async Task HistoryShowsFullyConfimedGoodsOnly()
        {
            Assert.Equal(
                new List<IGoodConfirmation>
                {
                     (await new MockReceptionGood("1", 4, "360601").FullyConfirmed()).Confirmation,
                     (await new ExtraConfirmedReceptionGood(
                        await new MockReceptionGood("4", 4, "360603").FullyConfirmed()
                     ).PartiallyConfirmed(1)).Confirmation,
                     (await new MockReception("1")
                        .Goods.UnkownGood("Unknown")
                        .PartiallyConfirmed(1)).Confirmation,
                },
                await new MockReception(
                    "1",
                    await new MockReceptionGood("1", 4, "360601").FullyConfirmed(),
                    await new MockReceptionGood("2", 8, "360602").PartiallyConfirmed(4),
                    (await new ExtraConfirmedReceptionGood(
                        await new MockReceptionGood("4", 4, "360603").FullyConfirmed()
                    ).PartiallyConfirmed(1)),
                    await new MockReception("1").Goods.UnkownGood("Unknown").PartiallyConfirmed(1)
                ).Confirmation()
                 .History()
                 .ToListAsync()
            );
        }

        [Fact]
        public async Task NotConfirmedOnly_ShowsNotConfirmedGoodsConfirmations()
        {
            Assert.Contains(
                (await new MockReceptionGood("2", 8, "360602").PartiallyConfirmed(4)).Confirmation,
                await new MockReception(
                    "1",
                    await new MockReceptionGood("1", 4, "360601").FullyConfirmed(),
                    await new MockReceptionGood("2", 8, "360602").PartiallyConfirmed(4)
                ).NotConfirmedOnly()
                 .ToListAsync()
            );
        }
    }
}
