using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Warehouse.Core
{
    public class WareouseIncludedStorages : IStorages
    {
        private readonly IStorages _origin;

        public WareouseIncludedStorages(IStorages origin)
        {
            _origin = origin;
        }

        public IEntities<IStorage> PutAway => _origin.PutAway;

        public IEntities<IStorage> Race => _origin.Race;

        public IEntities<IStorage> Reserve => _origin.Reserve;

        public async Task<IStorage> ByBarcodeAsync(string ean)
        {
            var storage = await _origin.Reserve.FirstOrDefaultAsync(x => x.Equals(ean));
            if (storage == null)
            {
                storage = await _origin.Race.FirstOrDefaultAsync(x => x.Equals(ean));
                if (storage == null)
                {
                    storage = await _origin.PutAway.FirstOrDefaultAsync(x => x.Equals(ean));
                    if (storage == null)
                    {
                        storage = await _origin.ByBarcodeAsync(ean);
                        if (storage == null)
                        {
                            throw new InvalidOperationException($"No storage found for this ean: {ean}");
                        }
                    }
                }
            }
            return storage;
        }

        public Task<IList<IStorage>> ToListAsync()
        {
            return _origin.ToListAsync();
        }

        public IEntities<IStorage> With(IFilter filter)
        {
            return new WareouseIncludedStorages(
                (IStorages)_origin.With(filter)
            );
        }
    }
}
