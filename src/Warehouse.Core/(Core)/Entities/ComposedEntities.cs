using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Warehouse.Core
{
    public class ComposedEntities<T> : IEntities<T>
    {
        private readonly IEnumerable<IEntities<T>> _multiEntities;

        public ComposedEntities(params IEntities<T>[] multiEntities)
            : this((IEnumerable<IEntities<T>>)multiEntities)
        {
        }

        public ComposedEntities(IEnumerable<IEntities<T>> multiEntities)
        {
            _multiEntities = multiEntities;
        }

        public async Task<IList<T>> ToListAsync()
        {
            var result = new List<T>();
            foreach (var entities in _multiEntities)
            {
                result.AddRange(await entities.ToListAsync());
            }
            return result;
        }

        public IEntities<T> With(IFilter filter)
        {
            return new ComposedEntities<T>(
                _multiEntities.Select(entities => entities.With(filter))
            );
        }
    }
}
