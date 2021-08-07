using System.Collections.Generic;
using System.Threading.Tasks;

namespace Warehouse.Core
{
    public interface IOrders
    {
        Task<IList<IOrder>> ToListAsync();

        Task<IOrder> AddAsync(IOrder newOrder);
    }
}
