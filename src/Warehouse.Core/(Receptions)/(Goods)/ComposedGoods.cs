using System.Collections.Generic;
using System.Threading.Tasks;

namespace Warehouse.Core
{
    public class ComposedGoods : IEntities<IReceptionGood>
    {
        private readonly IEntities<IReceptionGood> _original;
        private readonly IList<IReceptionGood> _unkownGoods;

        public ComposedGoods(IEntities<IReceptionGood> original, IList<IReceptionGood> unkownGoods)
        {
            _original = original;
            _unkownGoods = unkownGoods;
        }


        public async Task<IList<IReceptionGood>> ToListAsync()
        {
            var result = new List<IReceptionGood>(_unkownGoods);
            result.AddRange(await _original.ToListAsync());
            return result;
        }

        public IEntities<IReceptionGood> With(IFilter filter)
        {
            return new ComposedGoods(_original.With(filter), _unkownGoods);
        }
    }
}
