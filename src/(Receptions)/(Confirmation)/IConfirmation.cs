using System.Collections.Generic;
using System.Threading.Tasks;
using Warehouse.Core.Goods;

namespace Warehouse.Core.Receptions
{
    public interface IConfirmation
    {
        IReception Reception { get; }

        IConfirmationState State { get; }

        Task AddAsync(IGood goodToAdd, int quantity);

        Task RemoveAsync(IGood goodToRemove, int quantity);

        Task CommitAsync();

        Task<List<IGoodConfirmation>> ToListAsync();

        Task ClearAsync();
    }
}
