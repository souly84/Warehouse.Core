using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Warehouse.Core.Plugins;

namespace Warehouse.Core
{
    public class StatefulReceptionGoods : IReceptionGoods
    {
        private readonly IReception _reception;
        private readonly IReceptionGoods _goods;
        private readonly IKeyValueStorage _goodsState;
        private StatefulConfirmationProgress _confirmationProgress;

        public StatefulReceptionGoods(
           IReception reception,
           IKeyValueStorage goodsState)
            : this(reception, reception.Goods, goodsState)
        {
        }

        public StatefulReceptionGoods(
            IReception reception,
            IReceptionGoods goods,
            IKeyValueStorage goodsState)
            : this(
                  reception,
                  goods,
                  goodsState,
                  new StatefulConfirmationProgress(reception, goodsState)
              )
        {
        }

        public StatefulReceptionGoods(
            IReception reception,
            IReceptionGoods goods,
            IKeyValueStorage goodsState,
            StatefulConfirmationProgress confirmationProgress)
        {
            _reception = reception;
            _goods = goods;
            _goodsState = goodsState;
            _confirmationProgress = confirmationProgress;
        }

        public async Task<IList<IReceptionGood>> ToListAsync()
        {
            var goods = await _goods.ToListAsync();
            await _confirmationProgress.RestoreAsync(goods);
            return goods
                .Select(g => new StatefulReceptionGood(g, _goodsState, string.Empty))
                .ToList<IReceptionGood>();
        }

        public IReceptionGood UnkownGood(string barcode)
        {
            return new StatefulReceptionGood(
                _goods.UnkownGood(barcode),
                _goodsState,
                barcode
            );
        }

        public IEntities<IReceptionGood> With(IFilter filter)
        {
            return new StatefulReceptionGoods(
                _reception,
                (IReceptionGoods)_goods.With(filter),
                _goodsState,
                _confirmationProgress
            );
        }
    }
}
