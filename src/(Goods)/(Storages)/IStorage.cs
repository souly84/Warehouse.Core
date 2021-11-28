using System.Threading.Tasks;

namespace Warehouse.Core.Warehouse
{
    public interface IStorage
    {
        IEntities<IGood> Goods { get; }

        Task IncreaseAsync(IGood good);

        Task Decrease(IGood good);
    }
}
