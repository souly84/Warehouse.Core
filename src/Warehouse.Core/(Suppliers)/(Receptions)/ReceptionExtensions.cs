using System.Threading.Tasks;

namespace Warehouse.Core
{
    public static class ReceptionExtensions
    {
        public static IConfirmation Confirmation(this IReception reception)
        {
            return new ReceptionConfirmation(reception);
        }

        public static IConfirmation NotConfirmedOnly(this IReception reception)
        {
            return reception.Confirmation().NotConfirmedOnly();
        }

        public static Task<bool> ConfirmedAsync(this IReception reception)
        {
            return reception.Confirmation().DoneAsync();
        }

        public static ReceptionWithExtraConfirmedGoods WithExtraConfirmed(this IReception reception)
        {
            return new ReceptionWithExtraConfirmedGoods(reception.WithUnknown());
        }

        public static IReception WithoutInitiallyConfirmed(this IReception reception)
        {
            return new ReceptionWithoutInitiallyConfirmedGoods(reception);
        }

        public static ReceptionWithUnkownGoods WithUnknown(this IReception reception)
        {
            return new ReceptionWithUnkownGoods(reception);
        }

        public static async Task ValidateAsync(this IReception reception, IConfirmation confirmation)
        {
            var confirmationList = await confirmation.ToListAsync();
            await reception.ValidateAsync(
               await confirmationList.WhereAsync(
                   async confirmation => (await confirmation.State.ToEnumAsync()) != IConfirmationState.ConfirmationState.NotStarted
               )
            );
        }
    }
}
