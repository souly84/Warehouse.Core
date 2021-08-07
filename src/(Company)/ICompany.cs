using System;
using System.Threading.Tasks;

namespace Warehouse.Core
{
    public interface ICompany
    {
        ICustomers Customers { get; }
        IUsers Users { get; }
        IWarehouse Warehouse { get; }
        Task<IUser> LoginAsync(string userName, string password);
    }
}
