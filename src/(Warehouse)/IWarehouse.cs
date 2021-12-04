using MediaPrint;

namespace Warehouse.Core
{
    public interface IWarehouse : IPrintable
    {
        IEntities<IWarehouseGood> Goods { get; }
    }
}
