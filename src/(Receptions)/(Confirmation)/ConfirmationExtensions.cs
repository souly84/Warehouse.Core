using System.Linq;
using System.Threading.Tasks;

namespace Warehouse.Core.Receptions
{
    public static class ConfirmationExtensions
    {
        public static async Task<bool> DoneAsync(this IConfirmation confirmation)
        {
            var goods = await confirmation.ToListAsync();
            return goods.All(confirmation => confirmation.Done());
        }
    }
}
