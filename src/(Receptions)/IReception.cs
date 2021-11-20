using System.Collections.Generic;
using System.Threading.Tasks;
using Warehouse.Core.Goods;

namespace Warehouse.Core
{
    public interface IReception
    {
        IGoods Goods { get; }

        Task ValidateAsync(IList<IGoodConfirmation> goodsToValidate);
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

        public List<IGoodConfirmation> ValidatedGoods { get; } = new List<IGoodConfirmation>();

        public Task ValidateAsync(IList<IGoodConfirmation> goodsToValidate)
        {
            ValidatedGoods.AddRange(goodsToValidate);
            return Task.CompletedTask;
        }
    }
}
