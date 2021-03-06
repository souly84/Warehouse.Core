using System.Collections.Generic;
using System.Threading.Tasks;
using Warehouse.Core.Tests.Extensions;
using Xunit;

namespace Warehouse.Core.Tests
{
    public class ReceptionWithoutInitiallyConfirmedGoodsTests
    {
        [Fact]
        public async Task Confirmation_SkipInitiallyConfirmedGoods()
        {
            var reception = new MockReception(
                "1",
                new MockReceptionGood("1", 4),
                new MockReceptionGood("2", 8),
                new MockReceptionGood("3", 3),
                // this one is initially confirmed and should be skipped from the final comit
                await new MockReceptionGood("4", 3).FullyConfirmed(),
                // this one is initially extra confirmed and should be skipped from the final comit
                new MockReceptionGood("5", 3, "360602", confirmedQuantity: 4)
            );

            var receptionWithoutConfirmed = reception.WithoutInitiallyConfirmed();
            await receptionWithoutConfirmed.Confirmation().AddAsync(new MockReceptionGood("1", 4), 4);
            await receptionWithoutConfirmed.Confirmation().AddAsync(new MockReceptionGood("3", 3), 2);
            await receptionWithoutConfirmed.Confirmation().CommitAsync();
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
        public async Task InitiallyExtraConfirmedGoodsNotVisibleInTheList()
        {
            var reception = new MockReception(
                "1",
                new MockReceptionGood("1", 4),
                new MockReceptionGood("2", 8),
                new MockReceptionGood("3", 3),
                 // this one is initially extra confirmed and should not be visible in the list
                new MockReceptionGood("4", 3, "360602", confirmedQuantity: 4)
            );

            Assert.Equal(
                new List<IGoodConfirmation>
                {
                    new MockReceptionGood("1", 4).Confirmation,
                    new MockReceptionGood("2", 8).Confirmation,
                    new MockReceptionGood("3", 3).Confirmation,
                },
                await reception
                    .WithoutInitiallyConfirmed()
                    .NotConfirmedOnly()
                    .ToListAsync()
            );
        }

        [Fact]
        public async Task Confirmation_InitiallyConfirmed_WasExtraConfirmed()
        {
            var reception = new MockReception(
                "1",
                new MockReceptionGood("1", 2, "360600"),
                new MockReceptionGood("2", 8, "360601"),
                await new MockReceptionGood("4", 3, "360602").FullyConfirmed()
            );
            await reception
                .WithoutInitiallyConfirmed()
                .WithExtraConfirmed()
                .WithoutExtraConfirmedDuplicates()
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

        [Fact]
        public async Task ValidatesConfirmedGoods()
        {
            var reception = new MockReception(
                "1",
                new MockReceptionGood("1", 1, "1111"),
                new MockReceptionGood("2", 2, "2222"),
                new MockReceptionGood("3", 4, "3333")
            );
            await reception
                .WithExtraConfirmed()
                .WithoutInitiallyConfirmed()
                .WithoutExtraConfirmedDuplicates()
                .ConfirmAsync(
                    "UknownBarcode",
                    "1111",
                    "1111",
                    "2222"
                );
            Assert.Equal(
                new List<IGoodConfirmation>
                {
                    (await new MockReceptionGood("", 1000, "UknownBarcode", isUnknown: true).PartiallyConfirmed(1)).Confirmation,
                    (await new ExtraConfirmedReceptionGood(
                        new MockReceptionGood("1", 1, "1111")
                    ).PartiallyConfirmed(2)).Confirmation,
                    (await new MockReceptionGood("2", 2, "2222").PartiallyConfirmed(1)).Confirmation,
                },
                reception.ValidatedGoods
            );
        }
    }
}
