using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Warehouse.Core.Plugins;

namespace Warehouse.Core
{
    public class StatefulReceptionGoods : IReceptionGoods
    {
        private readonly ReceptionWithExtraConfirmedGoods _reception;
        private readonly IReceptionGoods _goods;
        private readonly IKeyValueStore _goodsState;

        public StatefulReceptionGoods(
           ReceptionWithExtraConfirmedGoods reception,
           IKeyValueStore goodsState)
            : this(reception, reception.Goods, goodsState)
        {
        }

        public StatefulReceptionGoods(
            ReceptionWithExtraConfirmedGoods reception,
            IReceptionGoods goods,
            IKeyValueStore goodsState)
        {
            _reception = reception;
            _goods = goods;
            _goodsState = goodsState;
        }

        public async Task<IList<IReceptionGood>> ToListAsync()
        {
            var goods = await _goods.ToListAsync();
            var statefulGoods = new List<IReceptionGood>();
            foreach (var key in _goodsState.Keys)
            {
                var goodsByKey = goods.Where(g => g.Id == key);
                if (!goodsByKey.Any())
                {
                    goodsByKey = await _reception.ByBarcodeAsync(key);
                    foreach (var goodByKey in goodsByKey)
                    {
                        goods.Add(goodByKey);
                    }
                }
                var value = Convert.ToInt32(_goodsState.Get(key));
                foreach (var goodByKey in goodsByKey)
                {
                    if (value > 0)
                    {
                        goodByKey.Confirmation.Increase(value);
                    }
                    else
                    {
                        goodByKey.Confirmation.Increase(Math.Abs(value));
                    }
                }
            }

            return goods
                .Select(g => new StatefulReceptionGood(g, _goodsState))
                .ToList<IReceptionGood>();
        }

        public IReceptionGood UnkownGood(string barcode)
        {
            return new StatefulReceptionGood(
                _goods.UnkownGood(barcode),
                _goodsState
            );
        }

        public IEntities<IReceptionGood> With(IFilter filter)
        {
            return new StatefulReceptionGoods(
                _reception,
                (IReceptionGoods)_goods.With(filter),
                _goodsState
            );
        }
    }
}
