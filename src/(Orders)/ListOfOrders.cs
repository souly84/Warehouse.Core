using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Warehouse.Core.Orders
{
    public class ListOfOrders : IOrders
    {
        private readonly IList<IOrder> _orders;

        public ListOfOrders(params IOrder[] orders)
        {
            _orders = new List<IOrder>(orders);
        }

        public Task<IOrder> AddAsync(IOrder newOrder)
        {
            _orders.Add(newOrder);
            return Task.FromResult(newOrder);
        }

        public Task<IList<IOrder>> ToListAsync()
        {
            return Task.FromResult(_orders);
        }
    }
}
