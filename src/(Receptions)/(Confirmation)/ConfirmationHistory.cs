using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Warehouse.Core.Goods;

namespace Warehouse.Core.Receptions
{
    public class ConfirmationHistory : IConfirmation
    {
        private readonly IConfirmation _confirmation;

        public ConfirmationHistory(IConfirmation confirmation)
        {
            _confirmation = confirmation;
        }

        public IReception Reception => _confirmation.Reception;

        public IConfirmationState State => _confirmation.State;

        public Task AddAsync(IGood goodToAdd)
        {
            return _confirmation.AddAsync(goodToAdd);
        }

        public Task ClearAsync()
        {
            return _confirmation.ClearAsync();
        }

        public Task CommitAsync()
        {
            return _confirmation.CommitAsync();
        }

        public Task RemoveAsync(IGood goodToRemove)
        {
            return _confirmation.RemoveAsync(goodToRemove);
        }

        public async Task<List<IGoodConfirmation>> ToListAsync()
        {
            var confirmedOnly = new List<IGoodConfirmation>();
            var goodConfirmations = await _confirmation.ToListAsync();
            foreach (var goodConfirmation in goodConfirmations)
            {
                if (await goodConfirmation.DoneAsync())
                {
                    confirmedOnly.Add(goodConfirmation);
                }
            }
            return confirmedOnly;
        }
    }
}
