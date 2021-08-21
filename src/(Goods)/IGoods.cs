using System.Collections.Generic;
using System.Threading.Tasks;
using Warehouse.Core.Core;

namespace Warehouse.Core
{
    public interface IGoods
    {
        Task<IList<IGood>> ToListAsync();

        IGoods With(IFilter filter);
    }
}
