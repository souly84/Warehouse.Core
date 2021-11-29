using System.Threading.Tasks;

namespace Warehouse.Core
{
    public interface IStorage
    {
        IEntities<IGood> Goods { get; }

        Task IncreaseAsync(IGood good, int quantity);

        Task DecreaseAsync(IGood good, int quantity);
    }
}
