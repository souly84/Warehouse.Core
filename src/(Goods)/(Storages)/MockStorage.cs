using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediaPrint;
using Warehouse.Core.Goods.Storages;

namespace Warehouse.Core
{
    public class MockStorage : IStorage
    {
        private readonly Dictionary<IGood, int> _goods;

        public MockStorage(params IGood [] goods)
            : this(ToDictionary(goods))
        {
        }

        public MockStorage(Dictionary<IGood, int> goods)
        {
            _goods = goods;
        }

        public IEntities<IGood> Goods => new ListOfEntities<IGood>(StorageGoods());

        public void PrintTo(IMedia media)
        {
            media.Put("Goods", StorageGoods());
        }

        public Task DecreaseAsync(IGood good, int quantity)
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

        public Task IncreaseAsync(IGood good, int quantity)
        {
            if (!_goods.ContainsKey(good))
            {
                _goods.Add(good, 0);
            }
            _goods[good] += quantity;
            return Task.CompletedTask;
        }

        private static Dictionary<IGood, int> ToDictionary(IEnumerable<IGood> goods)
        {
            var goodsInStore = new Dictionary<IGood, int>();
            foreach (var good in goods)
            {
                goodsInStore.Add(good, good.Quantity);
            }

            return goodsInStore;
        }

        private IEnumerable<IGood> StorageGoods()
        {
            return _goods.Keys.Select(
                good => new StorageGood(good, _goods[good])
            );
        }
    }
}
