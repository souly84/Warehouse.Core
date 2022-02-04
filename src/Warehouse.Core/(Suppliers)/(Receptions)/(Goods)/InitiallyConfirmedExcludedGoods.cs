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
        private List<IReceptionGood> _updatedGoods = new List<IReceptionGood>();

        public InitiallyConfirmedExcludedGoods(IReceptionGoods goods)
        {
            _goods = goods;
        }

        public async Task<IList<IReceptionGood>> ToListAsync()
        {
            var goods = await _goods.ToListAsync();
            await SetInitiallyConfirmedGoodsAsync(goods);
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
                .Where(confirmation => !IgnoredGood(confirmation.Good))
                .ToList();
        }

        //public Task AddAsync(IReceptionGood goodToAdd, int quantity)
        //{
        //    RefreshUpdatedGoodsCollection(goodToAdd);
        //    return _confirmation.AddAsync(goodToAdd, quantity);
        //}

        //public Task RemoveAsync(IReceptionGood goodToRemove, int quantity)
        //{
        //    RefreshUpdatedGoodsCollection(goodToRemove);
        //    return _confirmation.RemoveAsync(goodToRemove, quantity);
        //}

        //public Task ClearAsync()
        //{
        //    _updatedGoods.Clear();
        //    return _confirmation.ClearAsync();
        //}

        //public async Task CommitAsync()
        //{
        //    await _confirmation.CommitAsync();
        //    _updatedGoods.Clear();
        //    _initiallyConfirmedGoods = null;
        //}

        //public async Task<List<IGoodConfirmation>> ToListAsync()
        //{
        //    await SetInitiallyConfirmedGoods();
        //    var goodConfirmations = await _confirmation.ToListAsync();
        //    return goodConfirmations
        //        .Where(a => !IgnoredGood(a.Good))
        //        .ToList();
        //}

        private async Task SetInitiallyConfirmedGoodsAsync(IList<IReceptionGood> goods)
        {
            if (_initiallyConfirmedGoods == null)
            {
                _initiallyConfirmedGoods = new List<IReceptionGood>();
                _initiallyConfirmedGoods.AddRange(
                    await goods.WhereAsync(async good => await good.ConfirmedAsync())
                );
            }
        }

        private bool IgnoredGood(IReceptionGood good)
        {
            _ = _initiallyConfirmedGoods ?? throw new InvalidOperationException(
                "Initially confirmed goods collection should be initialized"
            );
            return _initiallyConfirmedGoods.Contains(good)
                && !_updatedGoods.Contains(good);
        }

        //private void RefreshUpdatedGoodsCollection(IReceptionGood goodToUpdate)
        //{
        //    if (!_updatedGoods.Contains(goodToUpdate))
        //    {
        //        _updatedGoods.Add(goodToUpdate);
        //    }
        //}
    }
}
