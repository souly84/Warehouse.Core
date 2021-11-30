using System.Collections.Generic;
using System.Threading.Tasks;
using Warehouse.Core.Goods;

namespace Warehouse.Core.Receptions
{
    public class NotConfirmedOnly : IConfirmation
    {
        private readonly IConfirmation _confirmation;

        public NotConfirmedOnly(IConfirmation confirmation)
        {
            _confirmation = confirmation;
        }

        public IReception Reception => _confirmation.Reception;

        public IConfirmationState State => _confirmation.State;

        public Task AddAsync(IGood goodToAdd, int quantity)
        {
            return _confirmation.AddAsync(goodToAdd, quantity);
        }

        public Task RemoveAsync(IGood goodToRemove, int quantity)
        {
            return _confirmation.RemoveAsync(goodToRemove, quantity);
        }

        public Task ClearAsync()
        {
            return _confirmation.ClearAsync();
        }

        public Task CommitAsync()
        {
            return _confirmation.CommitAsync();
        }

        public async Task<List<IGoodConfirmation>> ToListAsync()
        {
            var notConfirmedOnly = new List<IGoodConfirmation>();
            var goodConfirmations = await _confirmation.ToListAsync();
            foreach (var goodConfirmation in goodConfirmations)
            {
                if (!await goodConfirmation.DoneAsync())
                {
                    notConfirmedOnly.Add(goodConfirmation);
                }
            }
            return notConfirmedOnly;
        }
    }
}
