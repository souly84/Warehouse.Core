using System;
using System.Threading.Tasks;
using MediaPrint;

namespace Warehouse.Core
{
    public class MockWarehouse : IWarehouse
    {
        private readonly IEntities<IStorage> _storages;

        public MockWarehouse(IEntities<IWarehouseGood> goods, IEntities<IStorage> storages)
        {
            Goods = goods;
            _storages = storages;
        }

        public IEntities<IWarehouseGood> Goods { get; }

        public async Task<IStorage> ByBarcodeAsync(string ean)
        {
            var storage = await _storages.FirstOrDefaultAsync(storage => storage.Equals(ean));
            if (storage == null)
            {
                throw new InvalidOperationException($"No storage found for this ean: {ean}");
            }
            return storage;
        }

        public void PrintTo(IMedia media)
        {
            media
                .Put("Goods", Goods)
                .Put("Storages", _storages);
        }
    }
}
