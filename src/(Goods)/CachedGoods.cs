using System.Collections.Generic;
using System.Threading.Tasks;

namespace Warehouse.Core
{
    public class CachedGoods : IGoods
    {
        private readonly IGoods _goods;
        private IList<IGood> _cache;

        public CachedGoods(IGoods goods)
        {
            _goods = goods;
        }

        public async Task<IList<IGood>> ToListAsync()
        {
            if (_cache == null)
            {
                _cache = await _goods.ToListAsync();
            }
            return _cache;
        }

        public IGoods With(IFilter filter)
        {
            return new CachedGoods(_goods.With(filter));
        }
    }
}
