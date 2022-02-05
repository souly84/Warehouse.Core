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

        public async Task<IList<IReceptionGood>> ByBarcodeAsync(string barcodeData, bool ignoreConfirmed = false)
        {
            var unknownGoods = _unknownGoods.Where(x => x.Equals(barcodeData));
            if (unknownGoods.Any())
            {
                return unknownGoods.ToList();
            }
            var goods = await _reception.ByBarcodeAsync(barcodeData, ignoreConfirmed);
            if (goods.Any())
            {
                return goods;
            }
            var good = _reception.Goods.UnkownGood(barcodeData);
            _unknownGoods.Add(good);
            return new List<IReceptionGood> { good };
        }
    }
}
