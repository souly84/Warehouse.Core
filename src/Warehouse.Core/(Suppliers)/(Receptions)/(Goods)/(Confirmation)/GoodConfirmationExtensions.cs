using System.Threading.Tasks;

namespace Warehouse.Core
{
    public static class GoodConfirmationExtensions
    {
        public static async Task<bool> DoneAsync(this IGoodConfirmation confirmation)
        {
            var goodConfirmationState = await confirmation.State.ToEnumAsync();
            return goodConfirmationState == IConfirmationState.ConfirmationState.Confirmed;
        }

        public static async Task<bool> PartiallyAsync(this IGoodConfirmation confirmation)
        {
            var goodConfirmationState = await confirmation.State.ToEnumAsync();
            return goodConfirmationState == IConfirmationState.ConfirmationState.Partially;
        }
    }
}
