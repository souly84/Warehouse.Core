using System.Threading.Tasks;
using MediaPrint;

namespace Warehouse.Core
{
    public interface IWarehouse : IPrintable
    {
        IEntities<IWarehouseGood> Goods { get; }

        Task<IStorage> ByBarcodeAsync(string ean);
    }
}
