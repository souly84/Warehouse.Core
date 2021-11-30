namespace Warehouse.Core
{
    public interface ISupplier
    {
        IEntities<IReception> Receptions { get; }
    }
}
