using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Warehouse.Core.Plugins;

namespace Warehouse.Core
{
    public class ConfirmationProgress
    {
        private readonly IReception _reception;
        private readonly IKeyValueStorage _goodsState;

        public ConfirmationProgress(
            IReception reception,
            IKeyValueStorage goodsState)
        {
            _reception = reception;
            _goodsState = goodsState;
        }

        public async Task RestoreAsync(IList<IReceptionGood> goods)
        {
            foreach (var key in _goodsState.Keys)
            {
                var goodsByKey = goods.Where(g => g.Id == key);
                if (!goodsByKey.Any())
                {
                    var value = _goodsState.Get<int>(key);
                    while (value != 0)
                    {
                        goodsByKey = await _reception.ByBarcodeAsync(key);
                        foreach (var goodByKey in goodsByKey)
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

                    if (goodsByKey != null)
                    {
                        foreach (var goodByKey in goodsByKey)
                        {
                            goods.Add(goodByKey);
                        }
                    }
                }
                else
                {
                    Restore(goodsByKey, _goodsState.Get<int>(key));
                }
            }
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
