using System.Collections.Generic;
using System.Threading.Tasks;

namespace Warehouse.Core
{
    public interface IEntities<TEntity>
    {
        Task<IList<TEntity>> ToListAsync();

        IEntities<TEntity> With(IFilter filter);
    }
}
