using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Warehouse.Core
{
    public static class GoodsExtensions
    {
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
    }
}
