using System.Threading.Tasks;

namespace Warehouse.Core
{
    public interface IOrders : IEntities<IOrder>
    {
        Task<IOrder> AddAsync(IOrder newOrder);
    }
}
