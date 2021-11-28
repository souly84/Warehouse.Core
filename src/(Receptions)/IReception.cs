using System.Collections.Generic;
using System.Threading.Tasks;
using Warehouse.Core.Goods;

namespace Warehouse.Core
{
    public interface IReception
    {
        IEntities<IGood> Goods { get; }

        Task ValidateAsync(IList<IGoodConfirmation> goodsToValidate);
    }

    public class MockReception : IReception
    {
        public MockReception(params IGood[] goods)
            : this(new ListOfEntities<IGood>(goods))
        {
        }

        public MockReception(IEntities<IGood> goods)
        {
            Goods = goods;
        }

        public IEntities<IGood> Goods { get; }

        public List<IGoodConfirmation> ValidatedGoods { get; } = new List<IGoodConfirmation>();

        public Task ValidateAsync(IList<IGoodConfirmation> goodsToValidate)
        {
            ValidatedGoods.AddRange(goodsToValidate);
            return Task.CompletedTask;
        }
    }
}
