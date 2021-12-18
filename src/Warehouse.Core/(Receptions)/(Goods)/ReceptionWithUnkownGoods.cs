using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Warehouse.Core
{
    public class ReceptionWithUnkownGoods : IReception
    {
        private readonly IReception _reception;
        private readonly int _defaultMaxQuantity;
        private IList<IReceptionGood> _unknownGoods = new List<IReceptionGood>();

        public ReceptionWithUnkownGoods(IReception reception)
            : this(reception, 1000)
        {
        }

        public ReceptionWithUnkownGoods(IReception reception, int defaultMaxQuantity)
        {
            _reception = reception;
            _defaultMaxQuantity = defaultMaxQuantity;
        }

        public IEntities<IReceptionGood> Goods => new ComposedGoods(_reception.Goods, _unknownGoods);

        public Task ValidateAsync(IList<IGoodConfirmation> goodsToValidate)
        {
            return _reception.ValidateAsync(goodsToValidate);
        }

        public async Task<IReceptionGood> ByBarcodeAsync(string barcodeData)
        {
            var good = _unknownGoods.FirstOrDefault(x => x.Equals(barcodeData));
            if (good != null)
            {
                return good;
            }
            var goods = await _reception.Goods.ByBarcodeAsync(barcodeData);
            if (goods.Any())
            {
                return goods.First();
            }
            good = new MockReceptionGood("", _defaultMaxQuantity, barcodeData);
            _unknownGoods.Add(good);
            return good;
        }
    }
}
