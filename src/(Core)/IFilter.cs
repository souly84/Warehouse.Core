using System.Collections.Generic;

namespace Warehouse.Core.Core
{
    public interface IFilter
    {
        Dictionary<string, object> ToParams();
    }
}
