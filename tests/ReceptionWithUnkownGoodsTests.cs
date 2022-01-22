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
                new MockReceptionGood(1, 4, "360600"),
                await new ReceptionWithUnkownGoods(
                    new MockReception(
                        new MockReceptionGood(1, 4, "360600"),
                        new MockReceptionGood(2, 8)
                    )
                ).ByBarcodeAsync("360600")
            );
        }

        [Fact]
        public async Task SkippingAlreadyConfirmed()
        {
            var good = await new ReceptionWithUnkownGoods(
                new MockReception(
                    new MockReceptionGood(1, 4, "1111"),
                    await new MockReceptionGood(2, 1, "2222").FullyConfirmed(),
                    new MockReceptionGood(3, 1, "2222"),
                    new MockReceptionGood(4, 3, "3333")
                )
            ).ByBarcodeAsync("2222", true);
            Assert.False(
                await good.ConfirmedAsync()
            );
        }

        [Fact]
        public async Task CreatesNewGood_WhenNotFoundByBarcodeNumber()
        {
            Assert.Equal(
                new MockReceptionGood(0, 1000, "3606001"),
                await new ReceptionWithUnkownGoods(
                    new MockReception(
                        new MockReceptionGood(1, 4, "360600"),
                        new MockReceptionGood(2, 8)
                    )
                ).ByBarcodeAsync("3606001")
            );
        }

        [Fact]
        public async Task NotSkippingAlreadyConfirmed()
        {
            var good = await new ReceptionWithUnkownGoods(
                new MockReception(
                    new MockReceptionGood(1, 4, "1111"),
                    await new MockReceptionGood(2, 1, "2222").FullyConfirmed(),
                    new MockReceptionGood(3, 1, "2222"),
                    new MockReceptionGood(4, 3, "3333")
                )
            ).ByBarcodeAsync("2222");
            Assert.True(
                await good.ConfirmedAsync()
            );
        }

        [Fact]
        public async Task CreatesOnlyOnce_WhenNotFoundByBarcodeNumber()
        {
            var reception = new ReceptionWithUnkownGoods(
                new MockReception(
                    new MockReceptionGood(1, 4, "360600"),
                    new MockReceptionGood(2, 8)
                )
            );
            await reception.ByBarcodeAsync("3606001");
            await reception.ByBarcodeAsync("3606001");
            Assert.Equal(
                3,
                (await reception.Goods.ToListAsync()).Count
            );
        }
    }
}
