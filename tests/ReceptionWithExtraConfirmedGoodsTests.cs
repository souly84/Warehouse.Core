﻿using System.Threading.Tasks;
using MediaPrint;
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
                new MockReceptionGood(1, 4, "360600"),
                await new ReceptionWithExtraConfirmedGoods(
                    new ReceptionWithUnkownGoods(
                        new MockReception(
                            new MockReceptionGood(1, 4, "360600"),
                            new MockReceptionGood(2, 8)
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
                    new MockReceptionGood(1, 1, "360600")
                ),
                await new ReceptionWithExtraConfirmedGoods(
                    new ReceptionWithUnkownGoods(
                        new MockReception(
                            await new MockReceptionGood(1, 1, "360600").FullyConfirmed(),
                            new MockReceptionGood(2, 8)
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
                        await new MockReceptionGood(1, 1, "360600").FullyConfirmed(),
                        await new MockReceptionGood(2, 1, "360600").FullyConfirmed(),
                        new MockReceptionGood(3, 8)
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
                new MockReceptionGood(0, 1000, "3606001"),
                await new ReceptionWithExtraConfirmedGoods(
                   new ReceptionWithUnkownGoods(
                        new MockReception(
                            new MockReceptionGood(1, 4, "360600"),
                            new MockReceptionGood(2, 8)
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
                        await new MockReceptionGood(1, 1, "360600").FullyConfirmed(),
                        new MockReceptionGood(2, 8)
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
