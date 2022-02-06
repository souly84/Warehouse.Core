using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace Warehouse.Core.Plugins
{
    public class JObjectAsKeyStore : IKeyValueStorage
    {
        private readonly IKeyValueStorage _store;
        private readonly string _key;

        public JObjectAsKeyStore(IKeyValueStorage store, string key)
        {
            _store = store;
            _key = key;
        }

        public IList<string> Keys => _store
            .JsonFor(_key)
            .Properties()
            .Select(p => p.Name)
            .ToList();

        public bool Contains(string key)
        {
            return _store.JsonFor(_key)
                .Properties()
                .Any(p => p.Name == key);
        }

        public T Get<T>(string key)
        {
            return _store.JsonFor(_key).Value<T>(key) ?? default;
        }

        public void Remove(string key)
        {
            var jObject = _store.JsonFor(_key);
            jObject.Remove(key);
            _store.Set(_key, jObject);
        }

        public void Set<T>(string key, T @object)
        {
            JToken? token = @object as JToken;
            if (token == null)
            {
                token = new JValue(@object);
            }
            var jObject = _store.JsonFor(_key);
            jObject[key] = token;
            _store.Set(_key, jObject);
        }
    }
}
