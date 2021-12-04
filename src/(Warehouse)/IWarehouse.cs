using Warehouse.Core.Warehouse.Goods;

namespace Warehouse.Core
{
    public interface IWarehouse
    {
        IEntities<IWarehouseGood> Goods { get; }
    }
}
