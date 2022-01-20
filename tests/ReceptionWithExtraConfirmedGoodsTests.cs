using System.Threading.Tasks;
using Warehouse.Core.Tests.Extensions;
using Xunit;

namespace Warehouse.Core.Tests
{
    public class ReceptionWithExtraConfirmedGoodsTests
    {
        [Fact]
        public async Task ReturnsExisitingByBarcodeNumber()
        {
            Assert.Equal(
                new MockReceptionGood("good1", 4, "360600"),
                await new ReceptionWithExtraConfirmedGoods(
                    new ReceptionWithUnkownGoods(
                        new MockReception(
                            new MockReceptionGood("good1", 4, "360600"),
                            new MockReceptionGood("good2", 8)
                        )
                    )
                ).ByBarcodeAsync("360600")
            );
        }

        [Fact]
        public async Task CreatesExtraConfirmedGood_WhenConfirmedGoodBarcodeScanned()
        {
            Assert.Equal(
                new ExtraConfirmedReceptionGood(
                    new MockReceptionGood("good1", 1, "360600")
                ),
                await new ReceptionWithExtraConfirmedGoods(
                    new ReceptionWithUnkownGoods(
                        new MockReception(
                            await new MockReceptionGood("good1", 1, "360600").FullyConfirmed(),
                            new MockReceptionGood("good2", 8)
                        )
                    )
                ).ByBarcodeAsync("360600")
            );
        }

        [Fact]
        public async Task CreatesNewGood_WhenNotFoundByBarcodeNumber()
        {
            Assert.Equal(
                new MockReceptionGood("", 1000, "3606001"),
                await new ReceptionWithExtraConfirmedGoods(
                   new ReceptionWithUnkownGoods(
                        new MockReception(
                            new MockReceptionGood("good1", 4, "360600"),
                            new MockReceptionGood("good2", 8)
                        )
                    )
                ).ByBarcodeAsync("3606001")
            );
        }

        [Fact]
        public async Task ReturnsTheSameExtraConfirmedGood_WhenConfirmedGoodBarcodeScanned()
        {
            var reception = new ReceptionWithExtraConfirmedGoods(
                new ReceptionWithUnkownGoods(
                    new MockReception(
                        await new MockReceptionGood("good1", 1, "360600").FullyConfirmed(),
                        new MockReceptionGood("good2", 8)
                    )
                )
            );

            Assert.Same(
                await reception.ByBarcodeAsync("360600"),
                await reception.ByBarcodeAsync("360600")
            );
        }
    }
}
