using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Warehouse.Core
{
    public class ComposedStorages : IStorages
    {
        private readonly IEnumerable<IStorages> _multiStorages;

        public ComposedStorages(params IStorages[] multiStorages)
            : this((IEnumerable<IStorages>)multiStorages)
        {
        }

        public ComposedStorages(IEnumerable<IStorages> multiStorages)
        {
            _multiStorages = multiStorages;
        }

        public IEntities<IStorage> PutAway => new ComposedEntities<IStorage>(
            _multiStorages.Select(entities => entities.PutAway)
        );

        public IEntities<IStorage> Race => new ComposedEntities<IStorage>(
            _multiStorages.Select(entities => entities.Race)
        );

        public IEntities<IStorage> Reserve => new ComposedEntities<IStorage>(
            _multiStorages.Select(entities => entities.Reserve)
        );

        public async Task<IList<IStorage>> ToListAsync()
        {
            var result = new List<IStorage>();
            foreach (var storages in _multiStorages)
            {
                result.AddRange(await storages.ToListAsync());
            }
            return result;
        }

        public IEntities<IStorage> With(IFilter filter)
        {
            return new ComposedStorages(
                _multiStorages.Select(entities => (IStorages)entities.With(filter))
            );
        }
    }
}
