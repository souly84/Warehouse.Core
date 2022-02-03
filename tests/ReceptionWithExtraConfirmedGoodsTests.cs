using System.Collections.Generic;
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
                new MockReceptionGood("1", 4, "360600"),
                await new ReceptionWithExtraConfirmedGoods(
                    new ReceptionWithUnkownGoods(
                        new MockReception(
                            "1",
                            new MockReceptionGood("1", 4, "360600"),
                            new MockReceptionGood("2", 8)
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
                    new MockReceptionGood("1", 1, "360600")
                ),
                await new ReceptionWithExtraConfirmedGoods(
                    new ReceptionWithUnkownGoods(
                        new MockReception(
                            "1",
                            await new MockReceptionGood("1", 1, "360600").FullyConfirmed(),
                            new MockReceptionGood("2", 8)
                        )
                    )
                ).ByBarcodeAsync("360600")
            );
        }

        [Fact]
        public async Task CombinesExtraConfirmedGoodQuantity_WithOriginal()
        {
            var reception = new ReceptionWithExtraConfirmedGoods(
                new ReceptionWithUnkownGoods(
                    new MockReception(
                        "1",
                        await new MockReceptionGood("1", 1, "360600").FullyConfirmed(),
                        await new MockReceptionGood("2", 1, "360600").FullyConfirmed(),
                        new MockReceptionGood("3", 8)
                    )
                )
            );
            var good = await reception.ByBarcodeAsync("360600");
            good.Confirmation.Increase(3);
            Assert.Equal(
                5,
                good.Confirmation.ConfirmedQuantity
            );
        }

        [Fact]
        public async Task CreatesNewGood_WhenNotFoundByBarcodeNumber()
        {
            Assert.Equal(
                new MockReceptionGood("", 1000, "3606001", isUnknown: true),
                await new ReceptionWithExtraConfirmedGoods(
                   new ReceptionWithUnkownGoods(
                        new MockReception(
                            "1",
                            new MockReceptionGood("1", 4, "360600"),
                            new MockReceptionGood("2", 8)
                        )
                    )
                ).ByBarcodeAsync("3606001")
            );
        }

        [Fact]
        public async Task ExtraConfirmedGood_IsPartOfReceptionGoods()
        {
            var reception = new ReceptionWithExtraConfirmedGoods(
                new ReceptionWithUnkownGoods(
                    new MockReception(
                        "1",
                        await new MockReceptionGood("1", 1, "360600").FullyConfirmed(),
                        new MockReceptionGood("2", 8)
                    )
                )
            );
            await reception.ByBarcodeAsync("360600");
            Assert.Contains(
                new ExtraConfirmedReceptionGood(new MockReceptionGood("1", 1, "360600")),
                await reception.Goods.ToListAsync()
            );
        }

        [Fact]
        public async Task ReturnsTheSameExtraConfirmedGood_WhenConfirmedGoodBarcodeScanned()
        {
            var reception = new ReceptionWithExtraConfirmedGoods(
                new ReceptionWithUnkownGoods(
                    new MockReception(
                        "1",
                        await new MockReceptionGood("1", 1, "360600").FullyConfirmed(),
                        new MockReceptionGood("2", 8)
                    )
                )
            );

            Assert.Same(
                await reception.ByBarcodeAsync("360600"),
                await reception.ByBarcodeAsync("360600")
            );
        }

        [Fact]
        public async Task ValidatesConfirmedGoods()
        {
            var reception = new MockReception(
                "1",
                new MockReceptionGood("1", 2, "360600"),
                new MockReceptionGood("2", 8, "360601")
            );
            await new ReceptionWithExtraConfirmedGoods(
                new ReceptionWithUnkownGoods(reception)
            ).ConfirmAsync(
                "360600",
                "360600",
                "360600",
                "360600",
                "360601"
            );
            Assert.Equal(
                new List<IGoodConfirmation>
                {
                    (await new ExtraConfirmedReceptionGood(
                        new MockReceptionGood("1", 2, "360600")
                    ).PartiallyConfirmed(4)).Confirmation,
                    (await new MockReceptionGood("2", 8, "360601").PartiallyConfirmed(1)).Confirmation,
                },
                reception.ValidatedGoods
            );
        }
    }
}
