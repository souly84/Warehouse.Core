using System.Collections.Generic;
using System.Threading.Tasks;

namespace Warehouse.Core
{
    public interface IReceptionGoods : IEntities<IReceptionGood>
    {
        IReceptionGood UnkownGood(string _barcode);
    }

    public class MockReceptionGoods : IReceptionGoods
    {
        private readonly IEntities<IReceptionGood> _goods;

        public MockReceptionGoods(params IReceptionGood[] goods)
            : this(new ListOfEntities<IReceptionGood>(goods))
        {
        }

        public MockReceptionGoods(IEntities<IReceptionGood> goods)
        {
            _goods = goods;
        }

        public Task<IList<IReceptionGood>> ToListAsync()
        {
            return _goods.ToListAsync();
        }

        public IReceptionGood UnkownGood(string barcode)
        {
            return new MockReceptionGood("", 1000, barcode);
        }

        public IEntities<IReceptionGood> With(IFilter filter)
        {
            return new MockReceptionGoods(_goods.With(filter));
        }
    }
}
