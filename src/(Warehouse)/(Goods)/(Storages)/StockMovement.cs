using System.Threading.Tasks;
using Warehouse.Core.Goods.Storages;

namespace Warehouse.Core
{
    public class StockMovement : IMovement
    {
        private readonly IWarehouseGood _good;
        private readonly IStorage _fromStorage;

        public StockMovement(IWarehouseGood good)
            : this(good, new IncorrectStorage())
        {
        }

        public StockMovement(IWarehouseGood good, IStorage fromStorage)
        {
            _good = good;
            _fromStorage = fromStorage;
        }

        public IMovement From(IStorage storage)
        {
            return new StockMovement(_good, storage);
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
