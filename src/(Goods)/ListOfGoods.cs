using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Warehouse.Core.Goods
{
    public class ListOfGoods : IGoods
    {
        private readonly IEnumerable<IGood> _goods;

        public ListOfGoods(params IGood[] goods)
            : this((IEnumerable<IGood>)goods)
        {
        }

        public ListOfGoods(IEnumerable<IGood> goods)
        {
            _goods = goods;
        }

        public Task<IList<IGood>> ToListAsync()
        {
            return Task.FromResult<IList<IGood>>(new List<IGood>(_goods));
        }

        public IGoods With(IFilter filter)
        {
            throw new NotImplementedException();
        }
    }
}
