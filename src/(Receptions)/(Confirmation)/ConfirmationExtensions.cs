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

        public static async Task<bool> ExistsAsync(this IConfirmation confirmation, string barcode)
        {
            var goodsByBracode = await confirmation.Reception.Goods.ByBarcodeAsync(barcode);
            return goodsByBracode.Any();
        }

        public static async Task AddAsync(this IConfirmation confirmation, string barcode)
        {
            var goodsByBracode = await confirmation.Reception.Goods.ByBarcodeAsync(barcode);
            foreach (var good in goodsByBracode)
            {
                await confirmation.AddAsync(good);
            }
        }

        public static async Task RemoveAsync(this IConfirmation confirmation, string barcode)
        {
            var goodsByBracode = await confirmation.Reception.Goods.ByBarcodeAsync(barcode);
            foreach (var good in goodsByBracode)
            {
                await confirmation.RemoveAsync(good);
            }
        }
    }
}
