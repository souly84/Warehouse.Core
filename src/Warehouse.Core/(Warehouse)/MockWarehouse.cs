using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediaPrint;

namespace Warehouse.Core
{
    public class MockWarehouse : IWarehouse
    {
        private readonly IEnumerable<IStorage> _storages;

        public MockWarehouse(params IStorage[] storages)
        {
            _storages = storages;
        }

        public IEntities<IWarehouseGood> Goods => new StoragesGoods(_storages);

        public Task<IStorage> ByBarcodeAsync(string ean)
        {
            var storage = _storages.FirstOrDefault(storage => storage.Equals(ean));
            if (storage == null)
            {
                throw new InvalidOperationException($"No storage found for this ean: {ean}");
            }
            return Task.FromResult(storage);
        }

        public void PrintTo(IMedia media)
        {
            media
                .Put("Goods", Goods)
                .Put("Storages", _storages);
        }
    }
}
