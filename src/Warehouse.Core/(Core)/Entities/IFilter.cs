using System.Collections.Generic;

namespace Warehouse.Core
{
    public interface IFilter
    {
        Dictionary<string, object> ToParams();
    }
}
