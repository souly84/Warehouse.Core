using System.Collections.Generic;
using System.Threading.Tasks;

namespace Warehouse.Core
{
    public interface ISuppliers
    {
        Task<IList<ISupplier>> ToListAsync();

        IGoods With(IFilter filter);
    }
}
