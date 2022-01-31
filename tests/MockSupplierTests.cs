using System;
using MediaPrint;
using Xunit;

namespace Warehouse.Core.Tests
{
    public class MockSupplierTests
    {
        [Fact]
        public void FindByReceptionDate()
        {
            Assert.True(
                new MockSupplier(
                    new MockReception(DateTime.Now.AddDays(-2))
                ).Equals(DateTime.Now.AddDays(-2))
            );
        }

        [Fact]
        public void DoesNotFindByOtherReceptionDate()
        {
            Assert.False(
                new MockSupplier(
                    new MockReception(DateTime.Now.AddDays(-2))
                ).Equals(DateTime.Now.AddDays(2))
            );
        }

        [Fact]
        public void SupplierGetHashCode()
        {
            Assert.NotEqual(
                0,
                new MockSupplier(
                    new MockReception(DateTime.Now.AddDays(-2))
                ).GetHashCode()
            );
        }

        [Fact]
        public void EqualsTheSame()
        {
            var suppleir = new MockSupplier(
                new MockReception(DateTime.Now.AddDays(-2))
            );
            Assert.True(
                suppleir.Equals(suppleir)
            );
        }

        [Fact]
        public void ToJson()
        {
            Assert.EqualJson(
                @"{
                  ""name"": ""MockSupplier"",
                  ""receptions"": [
                    {
                      ""ReceptionDate"": ""2021-01-31T00:00:00"",
                      ""Goods"": [
                        {
                          ""Id"": ""1"",
                          ""Quantity"": ""8"",
                          ""IsUnknown"": false,
                          ""IsExtraConfirmed"": false,
                          ""Barcode"": ""1111""
                        }
                      ]
                    }
                  ]
                }",
                new MockSupplier(
                    new MockReception(
                        new DateTime(2021, 01, 31),
                        new MockReceptionGood("1", 8, "1111")
                    )
                ).ToJson().ToString()
            );
        }
    }
}
