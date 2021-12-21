using System.Collections.Generic;

namespace Warehouse.Core
{
    public interface IFilter
    {
        Dictionary<string, object> ToParams();
    }

    public class EmptyFilter : IFilter
    {
        public Dictionary<string, object> ToParams()
        {
            return new Dictionary<string, object>();
        }
    }
}
