using System.Threading.Tasks;
using Xunit;

namespace Warehouse.Core.Tests
{
    public class ReceptionWithUnkownGoodsTests
    {
        [Fact]
        public async Task ReturnsExisitingByBarcodeNumber()
        {
            Assert.Equal(
                new MockReceptionGood("good1", 4, "360600"),
                await new ReceptionWithUnkownGoods(
                    new MockReception(
                        new MockReceptionGood("good1", 4, "360600"),
                        new MockReceptionGood("good2", 8)
                    )
                ).ByBarcodeAsync("360600")
            );
        }

        [Fact]
        public async Task CreatesNewGood_WhenNotFoundByBarcodeNumber()
        {
            Assert.Equal(
                new MockReceptionGood("", 1000, "3606001"),
                await new ReceptionWithUnkownGoods(
                    new MockReception(
                        new MockReceptionGood("good1", 4, "360600"),
                        new MockReceptionGood("good2", 8)
                    )
                ).ByBarcodeAsync("3606001")
            );
        }

        [Fact]
        public async Task CreatesOnlyOnce_WhenNotFoundByBarcodeNumber()
        {
            var reception = new ReceptionWithUnkownGoods(
                new MockReception(
                    new MockReceptionGood("good1", 4, "360600"),
                    new MockReceptionGood("good2", 8)
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
