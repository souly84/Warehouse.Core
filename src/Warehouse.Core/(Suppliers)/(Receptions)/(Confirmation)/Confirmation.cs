using System.Collections.Generic;
using System.Threading.Tasks;

namespace Warehouse.Core
{
    /// <summary>
    /// This class is needed to reduce code duplication. Otherwise the PR is rejected by Sonar cloud.
    /// </summary>
    public abstract class Confirmation : IConfirmation
    {
        private readonly IConfirmation _confirmation;

        protected Confirmation(IConfirmation confirmation)
        {
            _confirmation = confirmation;
        }

        public IReception Reception => _confirmation.Reception;

        public IConfirmationState State => _confirmation.State;

        public Task AddAsync(IReceptionGood goodToAdd, int quantity)
        {
            return _confirmation.AddAsync(goodToAdd, quantity);
        }

        public Task RemoveAsync(IReceptionGood goodToRemove, int quantity)
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

        public abstract Task<List<IGoodConfirmation>> ToListAsync();
    }
}
