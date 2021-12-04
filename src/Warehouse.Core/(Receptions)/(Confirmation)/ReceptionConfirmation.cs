using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Warehouse.Core
{
    public class ReceptionConfirmation : IConfirmation
    {
        public ReceptionConfirmation(IReception reception)
        {
            Reception = reception;
        }

        public IReception Reception { get; }

        public IConfirmationState State => new GoodsConfirmationState(Reception);

        public async Task<List<IGoodConfirmation>> ToListAsync()
        {
            var goods = await Reception.Goods.ToListAsync();
            return goods
                .Select(good => good.Confirmation)
                .ToList();
        }

        public async Task AddAsync(IReceptionGood goodToAdd, int quantity)
        {
            var goods = await Reception
                .Goods
                .WhereAsync(good => good.Equals(goodToAdd));
            foreach (var good in goods)
            {
                good.Confirmation.Increase(quantity);
            }
        }

        public async Task RemoveAsync(IReceptionGood goodToRemove, int quantity)
        {
            var goods = await Reception
                .Goods
                .WhereAsync(good => good.Equals(goodToRemove));
            foreach (var good in goods)
            {
                good.Confirmation.Decrease(quantity);
            }
        }

        public async Task CommitAsync()
        {
            await Reception.ValidateAsync(this);
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
