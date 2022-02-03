using System.Collections.Generic;
using System.Threading.Tasks;

namespace Warehouse.Core
{
    public class ReceptionExtraGoods : IReceptionGoods
    {
        private readonly IReceptionGoods _goods;
        private readonly IList<IReceptionGood> _extraGoods;

        public ReceptionExtraGoods(IReceptionGoods goods, IList<IReceptionGood> extraGoods)
        {
            _goods = goods;
            _extraGoods = extraGoods;
        }

        public async Task<IList<IReceptionGood>> ToListAsync()
        {
            var result = new List<IReceptionGood>(_extraGoods);
            result.AddRange(await _goods.ToListAsync());
            return result;
        }

        public IReceptionGood UnkownGood(string barcode)
        {
            return _goods.UnkownGood(barcode);
        }

        public IEntities<IReceptionGood> With(IFilter filter)
        {
            return new ReceptionExtraGoods(
                (IReceptionGoods)_goods.With(filter),
                _extraGoods
            );
        }
    }
}
