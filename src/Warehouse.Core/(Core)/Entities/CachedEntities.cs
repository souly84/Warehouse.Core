using System.Collections.Generic;
using System.Threading.Tasks;

namespace Warehouse.Core
{
    public class CachedEntities<T> : IEntities<T>
    {
        private readonly IEntities<T> _origin;
        private IList<T>? _cache;

        public CachedEntities(IEntities<T> origin)
        {
            _origin = origin;
        }

        public async Task<IList<T>> ToListAsync()
        {
            if (_cache == null)
            {
                _cache = await _origin.ToListAsync();
            }
            return _cache;
        }

        public IEntities<T> With(IFilter filter)
        {
            return new CachedEntities<T>(_origin.With(filter));
        }
    }
}
