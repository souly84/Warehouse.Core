using System.Threading.Tasks;
using MediaPrint;
using Warehouse.Core.Warehouse.Goods;

namespace Warehouse.Core
{
    public interface IStorage : IPrintable
    {
        IEntities<IWarehouseGood> Goods { get; }

        Task IncreaseAsync(IWarehouseGood good, int quantity);

        Task DecreaseAsync(IWarehouseGood good, int quantity);
    }
}
