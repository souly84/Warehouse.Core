using System;
using System.Threading.Tasks;

namespace Warehouse.Core
{
    public static class StoragesExtension
    {
        public static async Task<IStorage> ByBarcodeAsync(this IStorages storages, string barcode)
        {
            var storage = await storages.Reserve.FirstOrDefaultAsync(x => x.Equals(barcode));
            if (storage == null)
            {
                storage = await storages.Race.FirstOrDefaultAsync(x => x.Equals(barcode));
                if (storage == null)
                {
                    storage = await storages.PutAway.FirstOrDefaultAsync(x => x.Equals(barcode));
                    if (storage == null)
                    {
                        throw new InvalidOperationException($"No storage found for this barcode: {barcode}");
                    }
                }
            }
            return storage;
        }
    }
}
