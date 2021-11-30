using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public async Task<List<IGoodConfirmation>> ToListAsync()
        {
            var confirmationTasks = goods
                .Select(good => good.FullyConfirmed())
                .ToList();
            await Task.WhenAll(confirmationTasks);
            return confirmationTasks
                .Select(task => task.Result.Confirmation)
                .ToList();
        }
    }
}
