using System.Collections.Generic;
using System.Threading.Tasks;
using Warehouse.Core.Goods;
using Warehouse.Core.Receptions.Goods;

namespace Warehouse.Core.Receptions
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
