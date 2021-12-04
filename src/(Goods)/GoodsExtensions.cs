using System.Collections.Generic;
using System.Threading.Tasks;
using Warehouse.Core.Goods;
using Warehouse.Core.Receptions.Goods;
using Warehouse.Core.Warehouse.Goods;

namespace Warehouse.Core
{
    public static class GoodsExtensions
    {
        public static Task<IEnumerable<IReceptionGood>> ByBarcodeAsync(
            this IEntities<IReceptionGood> goods,
            string barcode)
        {
            return goods.WhereAsync((good) => good.Equals(barcode));
        }

        public static Task<bool> ConfirmedAsync(this IReceptionGood good)
        {
            return good.Confirmation.DoneAsync();
        }

        public static IReceptionGood Clear(this IReceptionGood good)
        {
            good.Confirmation.Clear();
            return good;
        }

        public static IMovement From(this IWarehouseGood good, IStorage storage)
        {
            return good.Movement.From(storage);
        }
    }
}
