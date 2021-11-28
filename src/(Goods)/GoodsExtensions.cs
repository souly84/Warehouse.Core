using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Warehouse.Core.Goods;

namespace Warehouse.Core
{
    public static class GoodsExtensions
    {
        public static IGoods Cached(this IGoods goods)
        {
            return new CachedGoods(goods);
        }

        public static async Task<IEnumerable<IGood>> WhereAsync(
            this IGoods goods,
            Func<IGood, bool> predicate)
        {
            var goodsList = await goods.ToListAsync();
            return goodsList.Where(predicate);
        }

        public static Task<IEnumerable<IGood>> ByBarcodeAsync(
            this IGoods goods,
            string barcode)
        {
            return goods.WhereAsync((good) => good.Equals(barcode));
        }

        public static Task<bool> ConfirmedAsync(this IGood good)
        {
            return good.Confirmation.DoneAsync();
        }

        public static void Clear(this IGood good)
        {
            good.Confirmation.Clear();
        }
    }
}
