using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Warehouse.Core
{
    public class ReceptionWithExtraConfirmedGoods : IReception
    {
        private readonly ReceptionWithUnkownGoods _reception;
        private readonly int _defaultMaxQuantity;
        private readonly IList<IReceptionGood> _extraConfirmedGoods = new List<IReceptionGood>();

        public ReceptionWithExtraConfirmedGoods(ReceptionWithUnkownGoods reception)
            : this(reception, 1000)
        {
        }

        public ReceptionWithExtraConfirmedGoods(ReceptionWithUnkownGoods reception, int defaultMaxQuantity)
        {
            _reception = reception;
            _defaultMaxQuantity = defaultMaxQuantity;
        }

        public IReceptionGoods Goods => new ExtraReceptionGoods(_reception, _extraConfirmedGoods);

        public Task ValidateAsync(IList<IGoodConfirmation> goodsToValidate)
        {
            return _reception.ValidateAsync(
                new WithoutExtraConfirmedGoodDuplicates(goodsToValidate).ToList()
            );
        }

        public async Task<IReceptionGood> ByBarcodeAsync(string barcodeData)
        {
            var good = _extraConfirmedGoods.FirstOrDefault(x => x.Equals(barcodeData));
            if (good != null)
            {
                return good;
            }
            var goods = await _reception.Goods.ByBarcodeAsync(barcodeData);
            if (await NeedExtraGoodAsync(goods))
            {
                var extraGood = new ExtraConfirmedReceptionGood(goods.ToList(), _defaultMaxQuantity);
                _extraConfirmedGoods.Add(extraGood);
                return extraGood;
            }
            return await _reception.ByBarcodeAsync(barcodeData, true);
        }

        private async Task<bool> NeedExtraGoodAsync(IEnumerable<IReceptionGood> goods)
        {
            return goods.Any()
                && await goods.AllAsync(async x => await x.ConfirmedAsync());
        }
    }
}
