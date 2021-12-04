using MediaPrint;

namespace Warehouse.Core
{
    public interface ICustomer : IPrintable
    {
        IOrders Orders { get; }
    }
}
