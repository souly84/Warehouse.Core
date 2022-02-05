using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Warehouse.Core.Suppliers.Receptions.Goods;

namespace Warehouse.Core
{
    public class ReceptionWithoutInitiallyConfirmedGoods : IReception
    {
        private readonly IReception _reception;
        private InitiallyConfirmedExcludedGoods? _goods;

        public ReceptionWithoutInitiallyConfirmedGoods(IReception reception)
        {
            _reception = reception ?? throw new ArgumentNullException(nameof(reception));
        }

        public string Id => _reception.Id;

        public IReceptionGoods Goods => _goods ??= new InitiallyConfirmedExcludedGoods(_reception.Goods);

        public Task<IList<IReceptionGood>> ByBarcodeAsync(string barcodeData, bool ignoreConfirmed = false)
        {
            return _reception.ByBarcodeAsync(barcodeData, ignoreConfirmed);
        }

        public Task ValidateAsync(IList<IGoodConfirmation> goodsToValidate)
        {
            _ = _goods ?? throw new InvalidOperationException("Goods collection is not initialized. Goods property should be used first");
            return _reception.ValidateAsync(_goods.ExcludeInitiallyConfirmed(goodsToValidate));
        }
    }
}
