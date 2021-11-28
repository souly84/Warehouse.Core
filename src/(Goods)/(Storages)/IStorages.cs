using System.Collections.Generic;
using System.Threading.Tasks;

namespace Warehouse.Core.Warehouse
{
    public interface IStorages
    {
        Task<IList<IStorage>> ToListAsync();

        ISuppliers With(IFilter filter);
    }
}
