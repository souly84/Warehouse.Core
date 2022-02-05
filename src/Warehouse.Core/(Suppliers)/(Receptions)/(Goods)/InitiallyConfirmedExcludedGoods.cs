using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Warehouse.Core.Suppliers.Receptions.Goods
{
    public class InitiallyConfirmedExcludedGoods : IReceptionGoods
    {
        private readonly IReceptionGoods _goods;
        private List<IReceptionGood>? _initiallyConfirmedGoods;

        public InitiallyConfirmedExcludedGoods(IReceptionGoods goods)
        {
            _goods = goods;
        }

        public async Task<IList<IReceptionGood>> ToListAsync()
        {
            var goods = await _goods.ToListAsync();
            await SetupInitiallyConfirmedGoodsAsync(goods);
            return goods;
        }

        public IReceptionGood UnkownGood(string barcode)
        {
            return _goods.UnkownGood(barcode);
        }

        public IEntities<IReceptionGood> With(IFilter filter)
        {
            return new InitiallyConfirmedExcludedGoods((IReceptionGoods)_goods.With(filter));
        }

        public IList<IGoodConfirmation> ExcludeInitiallyConfirmed(IList<IGoodConfirmation> goodsToValidate)
        {
            return goodsToValidate
                .Where(confirmation => !InitiallyConfirmedGood(confirmation.Good))
                .ToList();
        }

        private async Task SetupInitiallyConfirmedGoodsAsync(IList<IReceptionGood> goods)
        {
            if (_initiallyConfirmedGoods == null)
            {
                _initiallyConfirmedGoods = new List<IReceptionGood>();
                _initiallyConfirmedGoods.AddRange(
                    await goods.WhereAsync(async good => await good.ConfirmedAsync())
                );
            }
        }

        private bool InitiallyConfirmedGood(IReceptionGood good)
        {
            _ = _initiallyConfirmedGoods ?? throw new InvalidOperationException(
                "Initially confirmed goods collection should be initialized."
            );
            var confirmedGood = _initiallyConfirmedGoods.FirstOrDefault(g => g.Equals(good));
            if (confirmedGood != null)
            {
                if (good.IsExtraConfirmed)
                {
                    ReduceAlreadyConfirmedQuantity(good, confirmedGood);
                    return false;
                }
                return true;
            }

            return false;
        }

        /// <summary>
        /// Need to reduce already confirmed quantity because only
        /// new confirmed quantity should be processed.
        /// </summary>
        private void ReduceAlreadyConfirmedQuantity(IReceptionGood goodToConfirm, IReceptionGood confirmedGood)
        {
            goodToConfirm
                .Confirmation
                .Decrease(confirmedGood.Confirmation.ConfirmedQuantity);
        }
    }
}
