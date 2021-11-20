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
                new ConfirmedGoods(
                    new MockGood("good1", 4),
                    new MockGood("good2", 8)
                ).ToList(),
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
                        ""Id"": ""good1""
                    },
                    ""Total"": ""4"",
                    ""Confirmed"": ""1""
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
                        ""Id"": ""good1""
                    },
                    ""Total"": ""4"",
                    ""Confirmed"": ""0""
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
                        ""Id"": ""good1""
                    },
                    ""Total"": ""4"",
                    ""Confirmed"": ""1""
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
                        ""Id"": ""good1""
                    },
                    ""Total"": ""4"",
                    ""Confirmed"": ""0""
                }",
                goodToConfirm.Confirmation.ToJson().ToString()
            );
        }
    }
}
