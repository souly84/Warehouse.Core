using System.Collections.Generic;
using System.Threading.Tasks;

namespace Warehouse.Core
{
    public class ReceptionWithoutExtraConfirmedGoodDuplicates : IReception
    {
        private readonly IReception _reception;

        public ReceptionWithoutExtraConfirmedGoodDuplicates(IReception reception)
        {
            _reception = reception;
        }

        public string Id => _reception.Id;

        public IReceptionGoods Goods => _reception.Goods;

        public Task<IList<IReceptionGood>> ByBarcodeAsync(string barcodeData, bool ignoreConfirmed = false)
        {
            return _reception.ByBarcodeAsync(barcodeData, ignoreConfirmed);
        }

        public Task ValidateAsync(IList<IGoodConfirmation> goodsToValidate)
        {
            return _reception.ValidateAsync(
                new WithoutExtraConfirmedGoodDuplicates(goodsToValidate).ToList()
            );
        }
    }
}
