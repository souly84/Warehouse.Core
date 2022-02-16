using System.Collections.Generic;
using System.Threading.Tasks;
using Warehouse.Core.Plugins;

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
            string barcode,
            bool ignoreConfirmed = false)
        {
            var filteredGoodsTask = goods.With(new EanGoodsFilter(barcode)).ToListAsync();
            if (!ignoreConfirmed)
            {
                return filteredGoodsTask;
            }
            return filteredGoodsTask.WhereAsync(async good => !await good.ConfirmedAsync());
        }

        public static IReceptionGoods Cached(
            this IReceptionGoods goods)
        {
            return new CachedReceptionGoods(goods);
        }

        public static Task<bool> ConfirmedAsync(this IReceptionGood good)
        {
            return good.Confirmation.DoneAsync();
        }

        public static IReceptionGood Stateful(
           this IReceptionGood good,
           IKeyValueStorage keyValueStorage)
        {
            return good.Stateful(keyValueStorage, string.Empty);
        }

        public static IReceptionGood Stateful(
            this IReceptionGood good,
            IKeyValueStorage keyValueStorage,
            string barcodeData)
        {
            return new StatefulReceptionGood(good, keyValueStorage, barcodeData);
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
