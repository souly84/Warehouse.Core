using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediaPrint;

namespace Warehouse.Core
{
    public class MockStorage : IStorage
    {
        private readonly string _storageEan;
        private readonly Dictionary<IWarehouseGood, int> _goods;

        public MockStorage(params IWarehouseGood[] goods) : this("1234567889", goods)
        {
        }

        public MockStorage(string storageEan, params IWarehouseGood[] goods)
            : this(storageEan, ToDictionary(goods))
        {
        }

        public MockStorage(string storageEan, Dictionary<IWarehouseGood, int> goods)
        {
            _storageEan = storageEan;
            _goods = goods;
        }

        public IEntities<IWarehouseGood> Goods => new ListOfEntities<IWarehouseGood>(StorageGoods());

        public void PrintTo(IMedia media)
        {
            media
                .Put("Ean", _storageEan)
                .Put("Goods", StorageGoods());
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

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj)
                || (obj is string ean && ean == _storageEan)
                || (obj is MockStorage storage && storage._storageEan == _storageEan);
        }

        public override int GetHashCode()
        {
            return _storageEan.GetHashCode();
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
                good => new MockWarehouseGood(good.ToDictionary().Value<string>("Id"), _goods[good])
            );
        }
    }
}
