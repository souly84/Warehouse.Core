using System.Collections.Generic;
using System.Threading.Tasks;
using Warehouse.Core.Tests.Extensions;
using Xunit;

namespace Warehouse.Core.Tests
{
    public class ReceptionWithUnkownGoodsTests
    {
        [Fact]
        public async Task ReturnsExisitingByBarcodeNumber()
        {
            Assert.Equal(
                new MockReceptionGood("1", 4, "360600"),
                await new ReceptionWithUnkownGoods(
                    new MockReception(
                        "1",
                        new MockReceptionGood("1", 4, "360600"),
                        new MockReceptionGood("2", 8)
                    )
                ).ByBarcodeAsync("360600")
                 .FirstAsync()
            );
        }

        [Fact]
        public async Task SkippingAlreadyConfirmed()
        {
            var good = await new ReceptionWithUnkownGoods(
                new MockReception(
                    "1",
                    new MockReceptionGood("1", 4, "1111"),
                    await new MockReceptionGood("2", 1, "2222").FullyConfirmed(),
                    new MockReceptionGood("3", 1, "2222"),
                    new MockReceptionGood("4", 3, "3333")
                )
            ).ByBarcodeAsync("2222", true)
             .FirstAsync();
            Assert.False(
                await good.ConfirmedAsync()
            );
        }

        [Fact]
        public async Task CreatesNewGood_WhenNotFoundByBarcodeNumber()
        {
            Assert.Equal(
                new MockReceptionGood("", 1000, "3606001", isUnknown: true),
                await new ReceptionWithUnkownGoods(
                    new MockReception(
                        "1",
                        new MockReceptionGood("1", 4, "360600"),
                        new MockReceptionGood("2", 8)
                    )
                ).ByBarcodeAsync("3606001")
                 .FirstAsync()
            );
        }

        [Fact]
        public async Task NotSkippingAlreadyConfirmed()
        {
            var good = await new ReceptionWithUnkownGoods(
                new MockReception(
                    "1",
                    new MockReceptionGood("1", 4, "1111"),
                    await new MockReceptionGood("2", 1, "2222").FullyConfirmed(),
                    new MockReceptionGood("3", 1, "2222"),
                    new MockReceptionGood("4", 3, "3333")
                )
            ).ByBarcodeAsync("2222")
             .FirstAsync();
            Assert.True(
                await good.ConfirmedAsync()
            );
        }

        [Fact]
        public async Task CreatesOnlyOnce_WhenNotFoundByBarcodeNumber()
        {
            var reception = new ReceptionWithUnkownGoods(
                new MockReception(
                    "1",
                    new MockReceptionGood("1", 4, "360600"),
                    new MockReceptionGood("2", 8)
                )
            );
            await reception.ByBarcodeAsync("3606001");
            await reception.ByBarcodeAsync("3606001");
            Assert.Equal(
                3,
                (await reception.Goods.ToListAsync()).Count
            );
        }

        [Fact]
        public async Task ValidatesConfirmedGoods()
        {
            var reception = new MockReception(
                "1",
                new MockReceptionGood("1", 4, "360600"),
                new MockReceptionGood("2", 8)
            );
            await new ReceptionWithUnkownGoods(reception)
                .ConfirmAsync(
                    "360600",
                    "360600",
                    "360600",
                    "360600",
                    "UnknownBarcode"
                );
            Assert.Equal(
                new List<IGoodConfirmation>
                {
                    (await new MockReceptionGood("", 1000, "UnknownBarcode", isUnknown: true).PartiallyConfirmed(1)).Confirmation,
                    (await new MockReceptionGood("1", 4, "360600").FullyConfirmed()).Confirmation,
                },
                reception.ValidatedGoods
            );
        }
    }
}
