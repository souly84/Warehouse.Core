using System.Collections.Generic;

namespace Warehouse.Core
{
    public interface IFilter
    {
        Dictionary<string, object> ToParams();

        bool Matches(object? entity);
    }

    public class EmptyFilter : IFilter
    {
        public bool Matches(object? entity)
        {
            return true;
        }

        public Dictionary<string, object> ToParams()
        {
            return new Dictionary<string, object>();
        }
    }
}
