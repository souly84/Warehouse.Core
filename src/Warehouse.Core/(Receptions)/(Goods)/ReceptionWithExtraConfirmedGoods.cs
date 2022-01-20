﻿using System.Collections.Generic;
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

        public IEntities<IReceptionGood> Goods => new ComposedGoods(_reception.Goods, _extraConfirmedGoods);

        public Task ValidateAsync(IList<IGoodConfirmation> goodsToValidate)
        {
            return _reception.ValidateAsync(goodsToValidate);
        }

        public async Task<IReceptionGood> ByBarcodeAsync(string barcodeData)
        {
            var good = _extraConfirmedGoods.FirstOrDefault(x => x.Equals(barcodeData));
            if (good != null)
            {
                return good;
            }
            var goods = await _reception.Goods.ByBarcodeAsync(barcodeData);
            if (await IsExtraGoodAsync(goods))
            {
                var extraGood = new ExtraConfirmedReceptionGood(goods.First(), _defaultMaxQuantity);
                _extraConfirmedGoods.Add(extraGood);
                return extraGood;
            }
            return await _reception.ByBarcodeAsync(barcodeData, true);
        }

        private async Task<bool> IsExtraGoodAsync(IEnumerable<IReceptionGood> goods)
        {
            return goods.Any()
                && await goods.AllAsync(async x => await x.ConfirmedAsync());
        }
    }
}
