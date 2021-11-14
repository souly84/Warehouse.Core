using System.Collections.Generic;
using System.Threading.Tasks;

namespace Warehouse.Core
{
    public interface IGoods
    {
        Task<IList<IGood>> ToListAsync();

        IGoods With(IFilter filter);
    }
}
