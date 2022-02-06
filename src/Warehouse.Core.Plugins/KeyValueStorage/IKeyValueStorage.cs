using System.Collections.Generic;

namespace Warehouse.Core.Plugins
{
    public interface IKeyValueStorage
    {
        IList<string> Keys { get; }

        void Set<T>(string key, T @object);

        T Get<T>(string key);

        void Remove(string key);

        bool Contains(string key);
    }
}
