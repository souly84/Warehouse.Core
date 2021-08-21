using System.Threading.Tasks;

namespace Warehouse.Core
{
    public interface IReception
    {
        IGoods Goods { get; }

        Task ConfirmAsync(IGood good);

        Task ValidateAsync();
    }
}
