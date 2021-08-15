using System;
using System.Threading.Tasks;
using Warehouse.Core.Orders;

namespace Warehouse.Core
{
    public interface ICompany
    {
        ICustomers Customers { get; }
        IUsers Users { get; }
        IWarehouse Warehouse { get; }
        Task<IUser> LoginAsync(string userName, string password);
    }

    public class MockedCompany : ICompany
    {
        public ICustomers Customers => throw new NotImplementedException();

        public IUsers Users => throw new NotImplementedException();

        public IWarehouse Warehouse => throw new NotImplementedException();

        public Task<IUser> LoginAsync(string userName, string password)
        {
            throw new NotImplementedException();
        }
    }

    public class MockedCustomer : ICustomer
    {
        public MockedCustomer(params IOrder[] orders) : this(new ListOfOrders(orders))
        {
        }
        public MockedCustomer(IOrders orders)
        {
            Orders = orders;
        }
        
        public IOrders Orders { get; }
    }
}
