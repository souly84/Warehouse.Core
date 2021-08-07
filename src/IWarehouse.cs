namespace Warehouse.Core
{
    public interface IWarehouse
    {
        IGoods Goods { get; }

        IReceptions Receptions { get; }
    }
}
