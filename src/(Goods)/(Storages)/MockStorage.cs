using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediaPrint;
using Warehouse.Core.Goods.Storages;
using Warehouse.Core.Warehouse.Goods;

namespace Warehouse.Core
{
    public class MockStorage : IStorage
    {
        private readonly Dictionary<IWarehouseGood, int> _goods;

        public MockStorage(params IWarehouseGood[] goods)
            : this(ToDictionary(goods))
        {
        }

        public MockStorage(Dictionary<IWarehouseGood, int> goods)
        {
            _goods = goods;
        }

        public IEntities<IWarehouseGood> Goods => new ListOfEntities<IWarehouseGood>(StorageGoods());

        public void PrintTo(IMedia media)
        {
            media.Put("Goods", StorageGoods());
        }

        public Task DecreaseAsync(IWarehouseGood good, int quantity)
        {
            if (_goods.ContainsKey(good))
            {
                _goods[good] -= quantity;
                if (_goods[good] == 0)
                {
                    _goods.Remove(good);
                }
            }
          
            return Task.CompletedTask;
        }

        public Task IncreaseAsync(IWarehouseGood good, int quantity)
        {
            if (!_goods.ContainsKey(good))
            {
                _goods.Add(good, 0);
            }
            _goods[good] += quantity;
            return Task.CompletedTask;
        }

        private static Dictionary<IWarehouseGood, int> ToDictionary(IEnumerable<IWarehouseGood> goods)
        {
            var goodsInStore = new Dictionary<IWarehouseGood, int>();
            foreach (var good in goods)
            {
                goodsInStore.Add(good, good.Quantity);
            }

            return goodsInStore;
        }

        private IEnumerable<IWarehouseGood> StorageGoods()
        {
            return _goods.Keys.Select(
                good => new StorageGood(good, _goods[good])
            );
        }
    }
}
