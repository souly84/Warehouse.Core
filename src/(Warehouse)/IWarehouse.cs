namespace Warehouse.Core
{
    public interface IWarehouse
    {
        IEntities<IWarehouseGood> Goods { get; }
    }
}
