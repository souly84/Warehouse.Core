using System.Collections.Generic;
using System.Threading.Tasks;
using Warehouse.Core.Tests.Extensions;
using Xunit;

namespace Warehouse.Core.Tests
{
    public class ReceptionWithInitiallyConfirmedExcludedGoodsTests
    {
        [Fact]
        public async Task Validation_SkipInitiallyConfirmedGoods()
        {
            var reception = new MockReception(
                "1",
                new MockReceptionGood("1", 4),
                new MockReceptionGood("2", 8),
                new MockReceptionGood("3", 3),
                // this one is initially confirmed and should be skipped from the final comit
                await new MockReceptionGood("4", 3).FullyConfirmed()
            );

            var excludedConfirmedGoodsReception = reception.ExcludeInitiallyConfirmed();
            await excludedConfirmedGoodsReception.Confirmation().AddAsync(new MockReceptionGood("1", 4), 4);
            await excludedConfirmedGoodsReception.Confirmation().AddAsync(new MockReceptionGood("3", 3), 2);
            await excludedConfirmedGoodsReception.Confirmation().CommitAsync();
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
        public async Task Validation_InitiallyConfirmed_WasExtraConfirmed()
        {
            var reception = new MockReception(
                "1",
                new MockReceptionGood("1", 2, "360600"),
                new MockReceptionGood("2", 8, "360601"),
                await new MockReceptionGood("4", 3, "360602").FullyConfirmed()
            );
            await reception
                .ExcludeInitiallyConfirmed()
                .WithExtraConfirmed()
                .ConfirmAsync(
                    "360601",
                    "360602"
                );

            Assert.Equal(
                new List<IGoodConfirmation>
                {
                    (await new ExtraConfirmedReceptionGood(
                        new MockReceptionGood("4", 3, "360602")
                    ).PartiallyConfirmed(1)).Confirmation,
                    (await new MockReceptionGood("2", 8, "360601").PartiallyConfirmed(1)).Confirmation
                },
                reception.ValidatedGoods
            );
        }
    }
}
