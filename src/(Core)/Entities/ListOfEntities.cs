using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Warehouse.Core.Goods
{
    public class ListOfEntities<TEntity> : IEntities<TEntity>
    {
        private readonly IEnumerable<TEntity> _entities;

        public ListOfEntities(params TEntity[] entities)
            : this((IEnumerable<TEntity>)entities)
        {
        }

        public ListOfEntities(IEnumerable<TEntity> entities)
        {
            _entities = entities;
        }

        public Task<IList<TEntity>> ToListAsync()
        {
            return Task.FromResult<IList<TEntity>>(new List<TEntity>(_entities));
        }

        public IEntities<TEntity> With(IFilter filter)
        {
            throw new NotImplementedException();
        }
    }
}
