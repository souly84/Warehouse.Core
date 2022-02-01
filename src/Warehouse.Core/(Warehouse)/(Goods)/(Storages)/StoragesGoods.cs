using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Warehouse.Core
{
    public class StoragesGoods : IEntities<IWarehouseGood>
    {
        private readonly IFilter _filter;
        private readonly IEnumerable<IStorage> _storages;

        public StoragesGoods(params IStorage[] storages)
            : this((IEnumerable<IStorage>)storages)
        {
        }

        public StoragesGoods(IEnumerable<IStorage> storages)
            : this(new EmptyFilter(), storages)
        {
        }

        public StoragesGoods(IFilter filter, params IStorage[] storages)
            : this(filter, (IEnumerable<IStorage>)storages)
        {
        }

        public StoragesGoods(IFilter filter, IEnumerable<IStorage> storages)
        {
            _filter = filter;
            _storages = storages;
        }

        public async Task<IList<IWarehouseGood>> ToListAsync()
        {
            var goodsList = new List<IWarehouseGood>();
            foreach (var storage in _storages)
            {
                var storageGoods = await storage.Goods.ToListAsync();
                goodsList.AddRange(storageGoods.Where(good => _filter.Matches(good)));
            }

            return goodsList;
        }

        public IEntities<IWarehouseGood> With(IFilter filter)
        {
            return new StoragesGoods(filter, _storages);
        }
    }
}
