using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Warehouse.Core.Tests.Extensions
{
    public class ConfirmedGoods
    {
        private readonly IReceptionGood[] goods;

        public ConfirmedGoods(params IReceptionGood[] goods)
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
