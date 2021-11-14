using System.Collections.Generic;
using System.Threading.Tasks;

namespace Warehouse.Core
{
    public interface IReceptions
    {
        Task<IList<IReception>> ToListAsync();

        IReceptions With(IFilter filter);
    }
}
