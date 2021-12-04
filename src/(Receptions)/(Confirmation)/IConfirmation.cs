using System.Collections.Generic;
using System.Threading.Tasks;

namespace Warehouse.Core
{
    public interface IConfirmation
    {
        IReception Reception { get; }

        IConfirmationState State { get; }

        Task AddAsync(IReceptionGood goodToAdd, int quantity);

        Task RemoveAsync(IReceptionGood goodToRemove, int quantity);

        Task CommitAsync();

        Task<List<IGoodConfirmation>> ToListAsync();

        Task ClearAsync();
    }
}
