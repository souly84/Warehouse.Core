using System.Threading.Tasks;
using MediaPrint;

namespace Warehouse.Core
{
    public interface IStorage : IPrintable
    {
        IEntities<IWarehouseGood> Goods { get; }

        Task<int> QuantityForAsync(IWarehouseGood good);

        Task IncreaseAsync(IWarehouseGood good, int quantity);

        Task DecreaseAsync(IWarehouseGood good, int quantity);
    }
}
