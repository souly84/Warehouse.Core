using System.Threading.Tasks;

namespace Warehouse.Core.Receptions
{
    public static class ReceptionExtensions
    {
        public static IConfirmation Confirmation(this IReception reception)
        {
            return new ReceptionConfirmation(reception);
        }

        public static IConfirmation NeedConfirmation(this IReception reception)
        {
            return reception.Confirmation().NeedConfirmation();
        }

        public static Task<bool> ConfirmedAsync(this IReception reception)
        {
            return reception.Confirmation().DoneAsync();
        }

        public static async Task ValidateAsync(this IReception reception, IConfirmation confirmation)
        {
            await reception.ValidateAsync(
                await confirmation.ToListAsync()
            );
        }
    }
}
