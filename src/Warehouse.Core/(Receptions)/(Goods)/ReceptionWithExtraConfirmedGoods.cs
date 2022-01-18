using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Warehouse.Core
{
    public class ReceptionWithExtraConfirmedGoods : IReception
    {
        private readonly IReception _reception;
        private readonly int _defaultMaxQuantity;
        private readonly IList<IReceptionGood> _extraConfirmedGoods = new List<IReceptionGood>();

        public ReceptionWithExtraConfirmedGoods(IReception reception)
            : this(reception, 1000)
        {
        }

        public ReceptionWithExtraConfirmedGoods(IReception reception, int defaultMaxQuantity)
        {
            _reception = reception;
            _defaultMaxQuantity = defaultMaxQuantity;
        }

        public IEntities<IReceptionGood> Goods => new ComposedGoods(_reception.Goods, _extraConfirmedGoods);

        public Task ValidateAsync(IList<IGoodConfirmation> goodsToValidate)
        {
            return _reception.ValidateAsync(goodsToValidate);
        }

        public async Task<IReceptionGood?> ByBarcodeAsync(string barcodeData)
        {
            var good = _extraConfirmedGoods.FirstOrDefault(x => x.Equals(barcodeData));
            if (good != null)
            {
                return good;
            }
            var goods = await _reception.Goods.ByBarcodeAsync(barcodeData);
            if (goods.Any())
            {
                if (await goods.AllAsync(async x => await x.ConfirmedAsync()))
                {
                    var extraGood = new ExtraConfirmedReceptionGood(goods.First(), _defaultMaxQuantity);
                    _extraConfirmedGoods.Add(extraGood);
                    return extraGood;
                }
            }
            return null;
        }
    }
}
