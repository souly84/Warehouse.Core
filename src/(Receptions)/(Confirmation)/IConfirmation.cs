using System.Collections.Generic;
using System.Threading.Tasks;
using Warehouse.Core.Goods;

namespace Warehouse.Core.Receptions
{
    public interface IConfirmation
    {
        IReception Reception { get; }

        Task AddAsync(IGood good);

        Task RemoveAsync(IGood good);

        Task CommitAsync();

        Task<List<IGoodConfirmation>> ToListAsync();

        Task ClearAsync();
    }
}
