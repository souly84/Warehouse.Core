using System.Linq;
using System.Threading.Tasks;
using Warehouse.Core.Receptions.Goods;

namespace Warehouse.Core.Receptions
{
    public static class ConfirmationExtensions
    {
        public static async Task<bool> DoneAsync(this IConfirmation confirmation)
        {
            var confirmationState = await confirmation.State.ToEnumAsync();
            return confirmationState == IConfirmationState.ConfirmationState.Confirmed;
        }

        public static IConfirmation History(this IConfirmation confirmation)
        {
            return new ConfirmationHistory(confirmation);
        }

        public static IConfirmation NeedConfirmation(this IConfirmation confirmation)
        {
            return new NotConfirmedOnly(confirmation);
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

        public static Task AddAsync(this IConfirmation confirmation, IReceptionGood good)
        {
            return confirmation.AddAsync(good, 1);
        }

        public static async Task RemoveAsync(this IConfirmation confirmation, string barcode)
        {
            var goodsByBracode = await confirmation.Reception.Goods.ByBarcodeAsync(barcode);
            foreach (var good in goodsByBracode)
            {
                await confirmation.RemoveAsync(good);
            }
        }

        public static Task RemoveAsync(this IConfirmation confirmation, IReceptionGood good)
        {
            return confirmation.RemoveAsync(good, 1);
        }
    }
}
