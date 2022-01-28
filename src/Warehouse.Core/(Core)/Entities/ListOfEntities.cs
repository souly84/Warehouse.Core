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
            return Task.FromResult(FilteredList());
        }

        public IEntities<TEntity> With(IFilter filter)
        {
            return new ListOfEntities<TEntity>(_entities, filter);
        }

        private IList<TEntity> FilteredList()
        {
            var filterParams = _filter.ToParams();
            var filteredEntites = new List<TEntity>();
            if (filterParams.Any())
            {
                foreach (var entity in _entities)
                {
                    if (_filter.Matches(entity))
                    {
                        filteredEntites.Add(entity);
                    }
                }
            }
            else
            {
                filteredEntites.AddRange(_entities);
            }
            
            return filteredEntites;
        }
    }
}
