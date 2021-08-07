namespace Warehouse.Core
{
    public interface IOrder
    {
        IGoods Goods { get; }

        IReceptions Receptions { get; }
    }
}
