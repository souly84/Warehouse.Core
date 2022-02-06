using Newtonsoft.Json.Linq;

namespace Warehouse.Core.Plugins
{
    public static class KeyValueStoreExtensions
    {
        public static JObject GetOrCreateJson(this IKeyValueStorage keyValueStore, string key)
        {
            if (!keyValueStore.Contains(key))
            {
                keyValueStore.Set(key, new JObject());
            }

            return keyValueStore.Get<JObject>(key);
        }
    }
}
