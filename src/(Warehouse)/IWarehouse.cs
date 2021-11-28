namespace Warehouse.Core
{
    public interface IWarehouse
    {
        IEntities<IGood> Goods { get; }
    }
}
