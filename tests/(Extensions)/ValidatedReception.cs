﻿using System.Threading.Tasks;
using Warehouse.Core.Receptions;

namespace Warehouse.Core.Tests.Extensions
{
    public class ValidatedReception<T> : IReception
        where T : IReception
    {
        private readonly T _origin;

        public ValidatedReception(T origin)
        {
            _origin = origin;
        }

        public IGoods Goods => _origin.Goods;

        public async Task<T> ValidateAsync()
        {
            var goodsToValidate = await _origin.Goods.ToListAsync();
            var validation = _origin.Validation();
            foreach (var good in goodsToValidate)
            {
                await validation.AddAsync(good);
            }

            await validation.CommitAsync();
            return _origin;
        }

        public Task ValidateAsync(IGoods goodsToValidate)
        {
            return _origin.ValidateAsync(goodsToValidate);
        }
    }
}
