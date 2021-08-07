using System.Collections.Generic;
using System.Threading.Tasks;

namespace Warehouse.Core
{
    public interface ICustomers
    {
        Task<IList<ICustomer>> ToListAsync();
    }
}
