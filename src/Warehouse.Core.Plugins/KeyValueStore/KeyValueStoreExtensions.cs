using Newtonsoft.Json.Linq;

namespace Warehouse.Core.Plugins
{
    public static class KeyValueStoreExtensions
    {
        public static void Set(this IKeyValueStore keyValueStore, string key, JObject @object)
        {
            keyValueStore.Set(key, @object.ToString());
        }

        public static JObject? GetJson(this IKeyValueStore keyValueStore, string key)
        {
            var jsonAsString = keyValueStore.Get(key);
            if (!string.IsNullOrEmpty(jsonAsString))
            {
                return JObject.Parse(jsonAsString);
            }
            return null;
        }

        public static JObject GetOrCreateJson(this IKeyValueStore keyValueStore, string key)
        {
            var result = keyValueStore.GetJson(key);
            if (result == null)
            {
                result = new JObject();
                keyValueStore.Set(key, result);
            }

            return result;
        }
    }
}
