using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Warehouse.Core
{
    public class ListOfEntities<TEntity> : IEntities<TEntity>
    {
        private readonly IEnumerable<TEntity> _entities;
        private readonly IFilter _filter;

        public ListOfEntities(params TEntity[] entities)
            : this(entities, new EmptyFilter())
        {
        }

        public ListOfEntities(IEnumerable<TEntity> entities)
            : this(entities, new EmptyFilter())
        {
        }

        public ListOfEntities(IEnumerable<TEntity> entities, IFilter filter)
        {
            _entities = entities;
            _filter = filter;
        }

        public Task<IList<TEntity>> ToListAsync()
        {
            return Task.FromResult<IList<TEntity>>(
                _entities
                    .Where(entity => _filter.Matches(entity))
                    .ToList()
            );
        }

        public IEntities<TEntity> With(IFilter filter)
        {
            return new ListOfEntities<TEntity>(_entities, filter);
        }
    }
}
