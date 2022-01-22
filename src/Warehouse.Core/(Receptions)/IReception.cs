using System.Collections.Generic;
using System.Threading.Tasks;

namespace Warehouse.Core
{
    public interface IReception
    {
        IReceptionGoods Goods { get; }

        Task ValidateAsync(IList<IGoodConfirmation> goodsToValidate);
    }

    public class MockReception : IReception
    {
        public MockReception(params IReceptionGood[] goods)
            : this(new ListOfEntities<IReceptionGood>(goods))
        {
        }

        public MockReception(IEntities<IReceptionGood> goods) : this(new MockReceptionGoods(goods))
        {
        }

        public MockReception(IReceptionGoods goods)
        {
            Goods = goods;
        }

        public IReceptionGoods Goods { get; }

        public List<IGoodConfirmation> ValidatedGoods { get; } = new List<IGoodConfirmation>();

        public Task ValidateAsync(IList<IGoodConfirmation> goodsToValidate)
        {
            ValidatedGoods.AddRange(goodsToValidate);
            return Task.CompletedTask;
        }
    }
}
