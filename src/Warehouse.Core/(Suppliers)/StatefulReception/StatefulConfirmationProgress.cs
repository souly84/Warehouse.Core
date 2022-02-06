using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Warehouse.Core.Plugins;

namespace Warehouse.Core
{
    public class StatefulConfirmationProgress
    {
        private readonly IReception _reception;
        private readonly IKeyValueStorage _goodsState;
        private bool _wasAlreadyRestored;

        public StatefulConfirmationProgress(
            IReception reception,
            IKeyValueStorage goodsState)
        {
            _reception = reception;
            _goodsState = goodsState;
        }

        /// <summary>
        /// Should be restore only once because good dont forget the confirmed quantity
        /// </summary>
        public async Task RestoreAsync(IList<IReceptionGood> goods)
        {
            if (_wasAlreadyRestored)
            {
                return;
            }
            _wasAlreadyRestored = true;
            foreach (var goodUniqueId in _goodsState.Keys)
            {
                var goodsById = goods.Where(g => g.Id == goodUniqueId);
                if (!goodsById.Any())
                {
                    goodsById = await RestoreByBarcode(goodUniqueId);
                    if (goodsById != null)
                    {
                        foreach (var goodByKey in goodsById)
                        {
                            goods.Add(goodByKey);
                        }
                    }
                }
                else
                {
                    Restore(goodsById, _goodsState.Get<int>(goodUniqueId));
                }
            }
        }

        private async Task<IEnumerable<IReceptionGood>?> RestoreByBarcode(string barcode)
        {
            IEnumerable<IReceptionGood>? goodsByBarcode = null;
            var value = _goodsState.Get<int>(barcode);
            while (value != 0)
            {
                goodsByBarcode = await _reception.ByBarcodeAsync(barcode);
                foreach (var goodByKey in goodsByBarcode)
                {
                    if (value > 0)
                    {
                        Restore(goodByKey, 1);
                        value--;
                    }
                    else
                    {
                        Restore(goodByKey, -1);
                        value++;
                    }
                }
            }
            return goodsByBarcode;
        }

        private void Restore(IEnumerable<IReceptionGood> goods, int value)
        {
            foreach (var good in goods)
            {
                Restore(good, value);
            }
        }

        private void Restore(IReceptionGood good, int value)
        {
            if (value > 0)
            {
                good.Confirmation.Increase(value);
            }
            else
            {
                good.Confirmation.Decrease(Math.Abs(value));
            }
        }
    }
}
