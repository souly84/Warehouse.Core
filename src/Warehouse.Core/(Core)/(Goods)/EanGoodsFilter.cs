using System;
using System.Collections.Generic;

namespace Warehouse.Core
{
    public class EanGoodsFilter : IFilter
    {
        private readonly string _goodEan;

        public EanGoodsFilter(string goodEan)
        {
            if (string.IsNullOrEmpty(goodEan))
            {
                throw new ArgumentNullException(nameof(goodEan));
            }
            _goodEan = goodEan;
        }

        public bool Matches(object? entity)
        {
            return entity != null && entity.Equals(_goodEan);
        }

        public Dictionary<string, object> ToParams()
        {
            return new Dictionary<string, object>
            {
                { "filter", "getProduct" },
                { "ean", _goodEan },
            };
        }
    }
}
