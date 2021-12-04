using System;
using System.Threading.Tasks;
using MediaPrint;
using Warehouse.Core.Warehouse.Goods;

namespace Warehouse.Core.Goods.Storages
{
    public class IncorrectStorage : IStorage
    {
        private readonly string _errorMessage;

        public IncorrectStorage()
            : this("This storage is incorrect, the real one should be initialized first.")
        {
        }

        public IncorrectStorage(string errorMessage)
        {
            _errorMessage = errorMessage;
        }

        public IEntities<IWarehouseGood> Goods => throw new InvalidOperationException(_errorMessage);

        public Task DecreaseAsync(IWarehouseGood good, int quantity)
        {
            throw new InvalidOperationException(_errorMessage);
        }

        public Task IncreaseAsync(IWarehouseGood good, int quantity)
        {
            throw new InvalidOperationException(_errorMessage);
        }

        public void PrintTo(IMedia media)
        {
            throw new InvalidOperationException(_errorMessage);
        }
    }
}
