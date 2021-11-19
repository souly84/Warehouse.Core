using System.Threading.Tasks;

namespace Warehouse.Core.Receptions
{
    public interface IValidation
    {
        IGoods Goods { get; }

        Task AddAsync(IGood good);

        Task RemoveAsync(IGood good);

        Task CommitAsync();
    }
}
