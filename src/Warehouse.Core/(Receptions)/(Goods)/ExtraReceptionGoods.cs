using System.Collections.Generic;
using System.Threading.Tasks;

namespace Warehouse.Core
{
    public class ExtraReceptionGoods : IReceptionGoods
    {
        private readonly IReception _reception;
        private readonly IList<IReceptionGood> _extraGoods;

        public ExtraReceptionGoods(IReception reception, IList<IReceptionGood> extraGoods)
        {
            _reception = reception;
            _extraGoods = extraGoods;
        }

        public async Task<IList<IReceptionGood>> ToListAsync()
        {
            var result = new List<IReceptionGood>(_extraGoods);
            result.AddRange(await _reception.Goods.ToListAsync());
            return result;
        }

        public IReceptionGood UnkownGood(string barcode)
        {
            return _reception.Goods.UnkownGood(barcode);
        }

        public IEntities<IReceptionGood> With(IFilter filter)
        {
            // we can loose the original entity here, its should not be working
            return _reception.Goods.With(filter);
        }
    }
}
