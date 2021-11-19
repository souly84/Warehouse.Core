using System.Collections.Generic;
using System.Threading.Tasks;
using Warehouse.Core.Goods;

namespace Warehouse.Core
{
    public interface IReception
    {
        IGoods Goods { get; }

        Task ValidateAsync(IGoods goodsToValidate);
    }

    public class MockReception : IReception
    {
        public MockReception(params IGood[] goods)
            : this(new ListOfGoods(goods))
        {
        }

        public MockReception(IGoods goods)
        {
            Goods = goods;
        }

        public IGoods Goods { get; }

        public List<IGood> ValidatedGoods { get; } = new List<IGood>();

        public async Task ValidateAsync(IGoods goodsToValidate)
        {
            ValidatedGoods.AddRange(await goodsToValidate.ToListAsync());
        }
    }
}
