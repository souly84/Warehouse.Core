using System.Threading.Tasks;

namespace Warehouse.Core.Goods
{
    public static class GoodConfirmationExtensions
    {
        public static async Task<bool> DoneAsync(this IGoodConfirmation confirmation)
        {
            var goodConfirmationState = await confirmation.State.ToEnumAsync();
            return goodConfirmationState == IConfirmationState.ConfirmationState.Confirmed;
        }
    }
}
