using System;
using System.Threading.Tasks;
using Warehouse.Core.Orders;
using System.Linq;
using Warehouse.Core.User;

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
        public MockedCompany(ICustomers customers, IUsers users, IWarehouse warehouse)
        {
            Customers = customers;
            Users = users;
            Warehouse = warehouse;
        }
        public ICustomers Customers { get; }

        public IUsers Users { get; }

        public IWarehouse Warehouse { get; }

        public Task<IUser> LoginAsync(string userName, string password)
        {
            return Users.LoginAsync(userName, password);
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
