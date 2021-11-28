using System.Threading.Tasks;

namespace Warehouse.Core.Warehouse
{
    public interface IStorage
    {
        string Number { get; }

        IGoods Goods { get; }

        Task IncreaseAsync(IGood good);

        Task Decrease(IGood good);
    }
}
