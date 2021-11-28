using System.Collections.Generic;
using System.Threading.Tasks;
using Warehouse.Core.Goods;

namespace Warehouse.Core
{
    public static class GoodsExtensions
    {
        public static Task<IEnumerable<IGood>> ByBarcodeAsync(
            this IEntities<IGood> goods,
            string barcode)
        {
            return goods.WhereAsync((good) => good.Equals(barcode));
        }

        public static Task<bool> ConfirmedAsync(this IGood good)
        {
            return good.Confirmation.DoneAsync();
        }

        public static IGood Clear(this IGood good)
        {
            good.Confirmation.Clear();
            return good;
        }
    }
}
