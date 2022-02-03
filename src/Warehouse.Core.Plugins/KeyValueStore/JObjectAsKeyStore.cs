using System.Collections.Generic;
using System.Linq;

namespace Warehouse.Core.Plugins
{
    public class JObjectAsKeyStore : IKeyValueStore
    {
        private readonly IKeyValueStore _store;
        private readonly string _key;

        public JObjectAsKeyStore(IKeyValueStore store, string key)
        {
            _store = store;
            _key = key;
        }

        public IList<string> Keys => _store
            .GetOrCreateJson(_key)
            .Properties()
            .Select(p => p.Name)
            .ToList();

        public bool Contains(string key)
        {
            return _store.GetOrCreateJson(_key).Properties().Any(p => p.Name == key);
        }

        public string Get(string key)
        {
            return _store.GetOrCreateJson(_key).Value<string>(key) ?? string.Empty;
        }

        public void Remove(string key)
        {
            _store.GetOrCreateJson(_key).Remove(key);
        }

        public void Set(string key, string @object)
        {
            _store.GetOrCreateJson(_key)[key] = @object;
        }
    }
}
