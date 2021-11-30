using System;
using System.Threading.Tasks;
using MediaPrint;

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

        public IEntities<IGood> Goods => throw new InvalidOperationException(_errorMessage);

        public Task DecreaseAsync(IGood good, int quantity)
        {
            throw new InvalidOperationException(_errorMessage);
        }

        public Task IncreaseAsync(IGood good, int quantity)
        {
            throw new InvalidOperationException(_errorMessage);
        }

        public void PrintTo(IMedia media)
        {
            throw new InvalidOperationException(_errorMessage);
        }
    }
}
