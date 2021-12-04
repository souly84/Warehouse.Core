using System.Collections.Generic;
using System.Threading.Tasks;

namespace Warehouse.Core
{
    public interface IReception
    {
        IEntities<IReceptionGood> Goods { get; }

        Task ValidateAsync(IList<IGoodConfirmation> goodsToValidate);
    }

    public class MockReception : IReception
    {
        public MockReception(params IReceptionGood[] goods)
            : this(new ListOfEntities<IReceptionGood>(goods))
        {
        }

        public MockReception(IEntities<IReceptionGood> goods)
        {
            Goods = goods;
        }

        public IEntities<IReceptionGood> Goods { get; }

        public List<IGoodConfirmation> ValidatedGoods { get; } = new List<IGoodConfirmation>();

        public Task ValidateAsync(IList<IGoodConfirmation> goodsToValidate)
        {
            ValidatedGoods.AddRange(goodsToValidate);
            return Task.CompletedTask;
        }
    }
}
