using System.Collections.Generic;
using System.Linq;
using Warehouse.Core.Goods;

namespace Warehouse.Core.Tests.Extensions
{
    public class ConfirmedGoods
    {
        private readonly IGood[] goods;

        public ConfirmedGoods(params IGood[] goods)
        {
            this.goods = goods;
        }

        public List<IGoodConfirmation> ToList()
        {
            return goods
                .Select(good => good.FullyConfirmed().Confirmation)
                .ToList();
        }
    }
}
