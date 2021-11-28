using System.Threading.Tasks;

namespace Warehouse.Core
{
    public interface ICompany
    {
        IEntities<ICustomer> Customers { get; }
        IEntities<IUser> Users { get; }
        IWarehouse Warehouse { get; }
        IEntities<ISupplier> Suppliers { get; }
        Task<IUser> LoginAsync(string userName, string password);
    }
}
