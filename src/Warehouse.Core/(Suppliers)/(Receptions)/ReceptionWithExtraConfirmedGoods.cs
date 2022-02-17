using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Warehouse.Core
{
    public class ReceptionWithExtraConfirmedGoods : IReception
    {
        private readonly IReception _reception;
        private readonly IList<IReceptionGood> _extraConfirmedGoods = new List<IReceptionGood>();

        public ReceptionWithExtraConfirmedGoods(IReception reception)
        {
            _reception = reception;
        }

        public IReceptionGoods Goods => new CombinedReceptionGoods(
            _reception.Goods,
            _extraConfirmedGoods
        );

        public string Id => _reception.Id;

        public Task ValidateAsync(IList<IGoodConfirmation> goodsToValidate)
        {
            return _reception.ValidateAsync(
                new WithoutExtraConfirmedGoodDuplicates(goodsToValidate).ToList()
            );
        }

        public async Task<IList<IReceptionGood>> ByBarcodeAsync(string barcodeData, bool ignoreConfirmed = false)
        {
            var extraConfirmedGoods = _extraConfirmedGoods.Where(x => x.Equals(barcodeData));
            if (extraConfirmedGoods.Any())
            {
                return extraConfirmedGoods.ToList();
            }
            var goods = await _reception.ByBarcodeAsync(barcodeData, false);
            if (await NeedExtraGoodAsync(goods))
            {
                var extraGood = new ExtraConfirmedReceptionGood(goods.ToList());
                _extraConfirmedGoods.Add(extraGood);
                return new List<IReceptionGood> { extraGood };
            }

            if (!ignoreConfirmed)
            {
                return goods;
            }
            return await _reception.ByBarcodeAsync(barcodeData, ignoreConfirmed);
        }

        private async Task<bool> NeedExtraGoodAsync(IEnumerable<IReceptionGood> goods)
        {
            return goods.Any()
                && await goods.AllAsync(async x => await x.ConfirmedAsync());
        }
    }
}
