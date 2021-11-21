using System.Collections.Generic;
using System.Threading.Tasks;

namespace Warehouse.Core
{
    public interface ISuppliers
    {
        Task<IList<ISupplier>> ToListAsync();

        ISuppliers With(IFilter filter);
    }
}
