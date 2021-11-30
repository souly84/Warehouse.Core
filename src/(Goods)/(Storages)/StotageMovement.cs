using System.Threading.Tasks;
using Warehouse.Core.Goods;
using Warehouse.Core.Goods.Storages;

namespace Warehouse.Core
{
    public class StotageMovement : IMovement
    {
        private readonly IGood _good;
        private readonly IStorage _fromStorage;

        public StotageMovement(IGood good)
            : this(good, new IncorrectStorage())
        {
        }

        public StotageMovement(IGood good, IStorage fromStorage)
        {
            _good = good;
            _fromStorage = fromStorage;
        }

        public IMovement From(IStorage storage)
        {
            return new StotageMovement(_good, storage);
        }

        public async Task MoveToAsync(IStorage storage, int quantity)
        {
            await _fromStorage.DecreaseAsync(_good, quantity);
            try
            {
                await storage.IncreaseAsync(_good, quantity);
            }
            catch
            {
                // Rollback on exception
                await _fromStorage.IncreaseAsync(_good, quantity);
                throw;
            }
        }
    }
}
