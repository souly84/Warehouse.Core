using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Warehouse.Core
{
    /// <summary>
    /// This entity is used when not expected good was scanned during reception validation operation.
    /// </summary>
    public class ReceptionWithUnkownGoods : IReception
    {
        private readonly IReception _reception;
        private readonly IList<IReceptionGood> _unknownGoods = new List<IReceptionGood>();

        public ReceptionWithUnkownGoods(IReception reception)
        {
            _reception = reception;
        }

        public IReceptionGoods Goods => new CombinedReceptionGoods(
            _reception.Goods,
            _unknownGoods
        );

        public string Id => _reception.Id;

        public Task ValidateAsync(IList<IGoodConfirmation> goodsToValidate)
        {
            return _reception.ValidateAsync(goodsToValidate);
        }

        public async Task<IReceptionGood> ByBarcodeAsync(string barcodeData, bool ignoreConfirmed = false)
        {
            var good = _unknownGoods.FirstOrDefault(x => x.Equals(barcodeData));
            if (good != null)
            {
                return good;
            }
            var goods = await _reception.Goods.ByBarcodeAsync(barcodeData);
            if (goods.Any())
            {
                if (ignoreConfirmed)
                {
                    return await goods.FirstAsync(async x => !await x.ConfirmedAsync());
                }
                return goods.First();
            }
            good = _reception.Goods.UnkownGood(barcodeData);
            _unknownGoods.Add(good);
            return good;
        }
    }
}
