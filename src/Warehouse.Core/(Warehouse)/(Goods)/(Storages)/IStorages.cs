using System.Collections.Generic;
using System.Threading.Tasks;

namespace Warehouse.Core
{
    public interface IStorages : IEntities<IStorage>
    {
        IEntities<IStorage> PutAway { get; }
        IEntities<IStorage> Race { get; }
        IEntities<IStorage> Reserve { get; }
    }

    public class MockStorages : IStorages
    {
        public MockStorages()
            : this(
                new ListOfEntities<IStorage>(),
                new ListOfEntities<IStorage>(),
                new ListOfEntities<IStorage>()
              )
        {
        }

        public MockStorages(
            IEntities<IStorage> putAway,
            IEntities<IStorage> race,
            IEntities<IStorage> reserve)
        {
            PutAway = putAway;
            Race = race;
            Reserve = reserve;
        }

        public IEntities<IStorage> PutAway { get; }

        public IEntities<IStorage> Race { get; }

        public IEntities<IStorage> Reserve { get; }

        public async Task<IList<IStorage>> ToListAsync()
        {
            var storageList = new List<IStorage>(await PutAway.ToListAsync());
            storageList.AddRange(await Race.ToListAsync());
            storageList.AddRange(await Reserve.ToListAsync());
            return storageList;
        }

        public IEntities<IStorage> With(IFilter filter)
        {
            return new MockStorages(
                PutAway.With(filter),
                Race.With(filter),
                Reserve.With(filter)
            );
        }
    }
}
