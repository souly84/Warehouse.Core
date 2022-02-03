﻿using System.Collections.Generic;
using System.Threading.Tasks;

namespace Warehouse.Core.Tests.Extensions
{
    public class ConfirmedReception<T> : IReception
        where T : IReception
    {
        private readonly T _origin;

        public ConfirmedReception(T origin)
        {
            _origin = origin;
        }

        public IReceptionGoods Goods => _origin.Goods;

        public string Id => _origin.Id;

        public async Task<T> ConfirmAsync()
        {
            var confirmation = _origin.Confirmation();
            foreach (var good in await _origin.Goods.ToListAsync())
            {
                while (!await good.ConfirmedAsync())
                {
                    await confirmation.AddAsync(good);
                }
            }

            await confirmation.CommitAsync();
            return _origin;
        }

        public Task ValidateAsync(IList<IGoodConfirmation> goodsToValidate)
        {
            return _origin.ValidateAsync(goodsToValidate);
        }
    }
}
