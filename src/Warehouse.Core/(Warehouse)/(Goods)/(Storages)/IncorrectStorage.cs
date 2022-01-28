using System;
using System.Threading.Tasks;
using MediaPrint;

namespace Warehouse.Core.Goods.Storages
{
    public class IncorrectStorage : IStorage
    {
        private readonly Exception _exception;

        public IncorrectStorage()
            : this("This storage is incorrect, the real one should be initialized first.")
        {
        }

        public IncorrectStorage(string errorMessage)
            : this(new InvalidOperationException(errorMessage))
        {
        }

        public IncorrectStorage(Exception exception)
        {
            _exception = exception;
        }

        public IEntities<IWarehouseGood> Goods => throw _exception;

        public Task DecreaseAsync(IWarehouseGood good, int quantity)
        {
            throw _exception;
        }

        public Task IncreaseAsync(IWarehouseGood good, int quantity)
        {
            throw _exception;
        }

        public void PrintTo(IMedia media)
        {
            // Nothing to do here
        }

        public Task<int> QuantityForAsync(IWarehouseGood good)
        {
            throw _exception;
        }
    }
}
