using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Warehouse.Core.Tests.Extensions;
using Xunit;

namespace Warehouse.Core.Tests
{
    public class StatefulReceptionTests
    {
        private IReception _reception = new MockReception(
            "1",
            new MockReceptionGood("Id1", 2, "360600"),
            new MockReceptionGood("Id2", 8, "360601"),
            new MockReceptionGood("Id4", 3, "360602").FullyConfirmed().RunSync()
        ).WithExtraConfirmed()
         .WithoutInitiallyConfirmed();

        [Fact]
        public async Task ConfirmationOperationsStoredInPersistanceLayer()
        {
            var persistanceLayer = new KeyValueStorage();
            await new StatefulReception(
                _reception,
                persistanceLayer
            ).ConfirmWithoutCommitAsync("360600", "360600");

            Assert.Equal(
                2,
                persistanceLayer
                    .Get<JObject>("Repcetion_1")
                    .Value<int>("Id1")
            );
        }

        [Fact]
        public async Task ConfirmationForExtraConfrimedGoodStoredInPersistanceLayer()
        {
            var persistanceLayer = new KeyValueStorage();
            await new StatefulReception(
                _reception,
                persistanceLayer
            ).ConfirmWithoutCommitAsync("360600", "360600", "360600", "360600");

            Assert.Equal(
                4,
                persistanceLayer
                    .Get<JObject>("Repcetion_1")
                    .Value<int>("360600")
            );
        }

        [Fact]
        public async Task ConfirmationForUnknownGoodStoredInPersistanceLayer()
        {
            var persistanceLayer = new KeyValueStorage();
            await new StatefulReception(
                _reception,
                persistanceLayer
            ).ConfirmWithoutCommitAsync("UnknownBarcode", "UnknownBarcode");

            Assert.Equal(
                2,
                persistanceLayer
                    .Get<JObject>("Repcetion_1")
                    .Value<int>("UnknownBarcode")
            );
        }

        [Fact]
        public async Task UnknownGoodConfirmationStoredInPersistanceLayer()
        {
            var persistanceLayer = new KeyValueStorage();
            await new StatefulReception(
                _reception,
                persistanceLayer
            ).ConfirmWithoutCommitAsync("UnknownBarcode", "UnknownBarcode");

            Assert.Equal(
                2,
                persistanceLayer
                    .Get<JObject>("Repcetion_1")
                    .Value<int>("UnknownBarcode")
            );
        }

        [Fact]
        public async Task ConfirmationRestoredFromPersistanceLayer()
        {
            var persistanceLayer = new KeyValueStorage();
            persistanceLayer.Set("Repcetion_1", JObject.Parse(@"{ ""Id2"" : 2 }"));
            persistanceLayer.Set("Repcetion_2", JObject.Parse(@"{ ""Id1"" : 2 }"));
            var goodsConfirmations = await new StatefulReception(
                _reception,
                persistanceLayer
            ).NotConfirmedOnly().ToListAsync();
            Assert.Equal(
                2,
                goodsConfirmations.Sum(goodsConfirmation => goodsConfirmation.ConfirmedQuantity)
            );
        }

        [Fact]
        public async Task ConfirmationForExtraConfirmedGoodRestoredFromPersistanceLayer()
        {
            var persistanceLayer = new KeyValueStorage();
            persistanceLayer.Set("Repcetion_1", JObject.Parse(@"{ ""360600"" : 3 }"));
            var good = await new StatefulReception(
                _reception,
                persistanceLayer
            ).Goods.ByBarcodeAsync("360600", true).FirstAsync();

            Assert.Equal(
                3,
                good.Confirmation.ConfirmedQuantity
            );
        }

        [Fact]
        public async Task ConfirmationForUnknownGoodRestoredFromPersistanceLayer()
        {
            var persistanceLayer = new KeyValueStorage();
            persistanceLayer.Set("Repcetion_1", JObject.Parse(@"{ ""UnknownBarcode"" : 3 }"));
            var good = await new StatefulReception(
                _reception,
                persistanceLayer
            ).Goods.ByBarcodeAsync("UnknownBarcode").FirstAsync();

            Assert.Equal(
                3,
                good.Confirmation.ConfirmedQuantity
            );
        }

        [Fact]
        public async Task PersistanceLayerClearedWhenConfirmationCommitted()
        {
            var persistanceLayer = new KeyValueStorage();
            await new StatefulReception(
                _reception,
                persistanceLayer
            ).ConfirmAsync("360600", "360600");

            Assert.False(
                persistanceLayer.Contains("Repcetion_1")
            );
        }

        [Fact]
        public async Task MultipleReceptionsSupported()
        {
            var persistanceLayer = new KeyValueStorage();
            await new StatefulReception(
                _reception,
                persistanceLayer
            ).ConfirmWithoutCommitAsync("360600", "360600");

            await new StatefulReception(
                new MockReception(
                    "2",
                    new MockReceptionGood("Id1", 2, "360600"),
                    new MockReceptionGood("Id2", 8, "360601"),
                    new MockReceptionGood("Id4", 3, "360602").FullyConfirmed().RunSync()
                ).WithExtraConfirmed()
                 .WithoutInitiallyConfirmed(),
                persistanceLayer
            ).ConfirmWithoutCommitAsync("360601", "360601");
            Assert.EqualJson(
                @"{ ""Id1"": 2 }",
                persistanceLayer.Get<JObject>("Repcetion_1").ToString()
            );

            Assert.EqualJson(
                @"{ ""Id2"": 2 }",
                persistanceLayer.Get<JObject>("Repcetion_2").ToString()
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
            await new StatefulReception(
                reception
                    .WithExtraConfirmed()
                    .WithoutInitiallyConfirmed(),
                new KeyValueStorage()
            ).ConfirmAsync(
                "UknownBarcode",
                "1111",
                "1111",
                "2222"
            );
            Assert.Equal(
                new List<IGoodConfirmation>
                {
                    (await new ExtraConfirmedReceptionGood(
                        new MockReceptionGood("1", 1, "1111")
                    ).PartiallyConfirmed(2)).Confirmation,
                    (await new MockReceptionGood("", 1000, "UknownBarcode", isUnknown: true).PartiallyConfirmed(1)).Confirmation,
                    (await new MockReceptionGood("2", 2, "2222").PartiallyConfirmed(1)).Confirmation,
                },
                reception.ValidatedGoods
            );
        }

        [Fact]
        public async Task RestoreReceptionState()
        {
            var reception = new MockReception(
                 "9",
                 await new MockReceptionGood("1", 1, "5449000131805").FullyConfirmed(),
                 await new MockReceptionGood("2", 1, "5410013108009").FullyConfirmed(),
                 await new MockReceptionGood("3", 1, "5410013108009").FullyConfirmed(),
                 await new MockReceptionGood("4", 1, "5410013108009").FullyConfirmed(),
                 new MockReceptionGood("5", 1, "5410013108009"),
                 await new MockReceptionGood("6", 1, "4005176891021").FullyConfirmed()
                 );
            var keyValueStorage = new KeyValueStorage();
            keyValueStorage.Set<JObject>("Repcetion_9", JObject.Parse(@"{
                  ""5449000131805"": 1,
                  ""5410013108009"": 1,
                  ""4005176891021"": 1
                }"));

            var result = await new StatefulReception(reception
                .WithExtraConfirmed()
                .WithoutInitiallyConfirmed(),
                keyValueStorage)
                .NotConfirmedOnly()
                .ToListAsync();

            Assert.Equal(
                4,
                result.Sum(x => x.ConfirmedQuantity)
                );
        }
    }
}
