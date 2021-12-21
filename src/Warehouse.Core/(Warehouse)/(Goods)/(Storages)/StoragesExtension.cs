using System;
using System.Threading.Tasks;

namespace Warehouse.Core
{
    public static class StoragesExtension
    {
        public static async Task<IStorage> ByBarcodeAsync(
            this IStorages storages,
            IWarehouse warehouse,
            string ean)
        {
            try
            {
                return await storages.ByBarcodeAsync(ean);
            }
            catch (InvalidOperationException)
            {
                return await warehouse.ByBarcodeAsync(ean);
            }
        }

        public static async Task<IStorage> ByBarcodeAsync(this IStorages storages, string ean)
        {
            var storage = await storages.Reserve.FirstOrDefaultAsync(x => x.Equals(ean));
            if (storage == null)
            {
                storage = await storages.Race.FirstOrDefaultAsync(x => x.Equals(ean));
                if (storage == null)
                {
                    storage = await storages.PutAway.FirstOrDefaultAsync(x => x.Equals(ean));
                    if (storage == null)
                    {
                        throw new InvalidOperationException($"No storage found for this ean: {ean}");
                    }
                }
            }
            return storage;
        }
    }
}
