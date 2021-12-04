using System.Threading.Tasks;
using MediaPrint;

namespace Warehouse.Core
{
    public interface ICompany : IPrintable
    {
        IEntities<ICustomer> Customers { get; }
        IEntities<IUser> Users { get; }
        IWarehouse Warehouse { get; }
        IEntities<ISupplier> Suppliers { get; }
        Task<IUser> LoginAsync(string userName, string password);
    }
}
