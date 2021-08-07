namespace Warehouse.Core
{
    public interface ICustomer
    {
        IOrders Orders { get; }
    }
}
