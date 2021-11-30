namespace Warehouse.Core
{
    public interface IOrder
    {
        IEntities<IGood> Goods { get; }

        IEntities<IReception> Receptions { get; }
    }
}
