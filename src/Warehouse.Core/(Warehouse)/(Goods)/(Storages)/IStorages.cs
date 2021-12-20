using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Warehouse.Core
{
    public interface IStorages : IEntities<IStorage>
    {
        IEntities<IStorage> PutAway { get; }
        IEntities<IStorage> Race { get; }
        IEntities<IStorage> Reserve { get; }

        Task<IStorage> ByBarcodeAsync(string ean);
    }

    public class MockStorages : IStorages
    {
        private readonly IEntities<IStorage> _remote;

        public MockStorages()
            : this(
                new ListOfEntities<IStorage>(),
                new ListOfEntities<IStorage>(),
                new ListOfEntities<IStorage>(),
                new ListOfEntities<IStorage>()
              )
        {
        }

        public MockStorages(
            IEntities<IStorage> putAway,
            IEntities<IStorage> race,
            IEntities<IStorage> reserve,
            IEntities<IStorage> remote)
        {
            PutAway = putAway;
            Race = race;
            Reserve = reserve;
            _remote = remote;
        }

        public IEntities<IStorage> PutAway { get; }

        public IEntities<IStorage> Race { get; }

        public IEntities<IStorage> Reserve { get; }

        public async Task<IStorage> ByBarcodeAsync(string ean)
        {
            var storages = await _remote.ToListAsync();
            return storages.First(s => s.Equals(ean));
        }

        public async Task<IList<IStorage>> ToListAsync()
        {
            var storageList = new List<IStorage>();
            storageList.AddRange(await PutAway.ToListAsync());
            storageList.AddRange(await Race.ToListAsync());
            storageList.AddRange(await Reserve.ToListAsync());
            return storageList;
        }

        public IEntities<IStorage> With(IFilter filter)
        {
            return new MockStorages(
                PutAway.With(filter),
                Race.With(filter),
                Reserve.With(filter),
                _remote.With(filter)
            );
        }
    }
}
