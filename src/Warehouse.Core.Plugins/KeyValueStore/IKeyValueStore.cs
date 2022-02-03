using System.Collections.Generic;

namespace Warehouse.Core.Plugins
{
    public interface IKeyValueStore
    {
        IList<string> Keys { get; }

        void Set(string key, string @object);

        string Get(string key);

        void Remove(string key);

        bool Contains(string key);
    }
}
