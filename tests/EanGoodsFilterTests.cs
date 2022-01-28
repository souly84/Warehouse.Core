using System;
using System.Collections.Generic;
using MediaPrint;
using Warehouse.Core.Tests.Extensions;
using Xunit;

namespace Warehouse.Core.Tests
{
    public class EanGoodsFilterTests
    {
        [Fact]
        public void ArgumentNullException_WhenEmptyEan()
        {
            Assert.Throws<ArgumentNullException>(() => new EanGoodsFilter(string.Empty));
        }

        [Fact]
        public void ToParams()
        {
            Assert.Equal(
                new DictionaryMedia()
                    .With("filter", "getProduct")
                    .With("ean", "1111"),
                new EanGoodsFilter("1111").ToDictionary()
            );
        }

        [Fact]
        public void FalseMatchForNull()
        {
            Assert.False(
                new EanGoodsFilter("1111").Matches(null)
            );
        }
    }
}
