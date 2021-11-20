using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Warehouse.Core.Goods;

namespace Warehouse.Core.Receptions
{
    public class ReceptionConfirmation : IConfirmation
    {
        public ReceptionConfirmation(IReception reception)
        {
            Reception = reception;
        }

        public IReception Reception { get; }

        public async Task<List<IGoodConfirmation>> ToListAsync()
        {
            var goods = await Reception.Goods.ToListAsync();
            return goods
                .Select(good => good.Confirmation)
                .ToList();
        }

        public async Task AddAsync(IGood goodToAdd)
        {
            var goods = await Reception
                .Goods
                .WhereAsync(good => good.Equals(goodToAdd));
            foreach (var good in goods)
            {
                good.Confirmation.Increase(1);
            }
        }

        public async Task RemoveAsync(IGood goodToDecrease)
        {
            var goods = await Reception
                .Goods
                .WhereAsync(good => good.Equals(goodToDecrease));
            foreach (var good in goods)
            {
                good.Confirmation.Decrease(1);
            }
        }

        public async Task CommitAsync()
        {
            await Reception.ValidateAsync(await ToListAsync());
        }

        public async Task ClearAsync()
        {
            var goods = await Reception
                .Goods.ToListAsync();
            foreach(var good in goods)
            {
                good.Confirmation.Clear();
            }
        }
    }
}
