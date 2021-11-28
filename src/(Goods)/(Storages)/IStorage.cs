using System.Threading.Tasks;

namespace Warehouse.Core.Warehouse
{
    public interface IStorage
    {
        string Number { get; }

        IEntities<IGood> Goods { get; }

        Task IncreaseAsync(IGood good);

        Task Decrease(IGood good);
    }
}
