using System.Threading.Tasks;

namespace Warehouse.Core.Goods
{
    public interface IMovement
    {
        Task MoveToAsync(IStorage storage, int quantity);

        IMovement From(IStorage storage);
    }
}
