namespace Warehouse.Core
{
    public interface IOrder
    {
        IEntities<IReceptionGood> Goods { get; }

        IEntities<IReception> Receptions { get; }
    }
}
