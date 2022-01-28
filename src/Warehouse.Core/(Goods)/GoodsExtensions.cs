using System.Collections.Generic;
using System.Threading.Tasks;

namespace Warehouse.Core
{
    public static class GoodsExtensions
    {
        public static IEntities<IWarehouseGood> For(this IEntities<IWarehouseGood> goods, string ean)
        {
            return goods.With(new EanGoodsFilter(ean));
        }
        
        public static Task<IList<IReceptionGood>> ByBarcodeAsync(
            this IEntities<IReceptionGood> goods,
            string barcode)
        {
            return goods.With(new EanGoodsFilter(barcode)).ToListAsync();
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
