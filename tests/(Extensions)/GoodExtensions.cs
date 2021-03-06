using System.Threading.Tasks;

namespace Warehouse.Core.Tests.Extensions
{
    public static class GoodExtensions
    {
        public static async Task<IReceptionGood> FullyConfirmed(this IReceptionGood good)
        {
            while (!await good.ConfirmedAsync())
            {
                good.Confirmation.Increase(1);
            }
            return good;
        }

        public static async Task<IGoodConfirmation> FullyConfirmed(this IGoodConfirmation goodConfirmation)
        {
            while (!await goodConfirmation.Good.ConfirmedAsync())
            {
                goodConfirmation.Increase(1);
            }
            return goodConfirmation;
        }

        public static async Task<IReceptionGood> PartiallyConfirmed(this IReceptionGood good, int confirmedQty)
        {
            if (!await good.ConfirmedAsync() || good is ExtraConfirmedReceptionGood)
            {
                good.Confirmation.Increase(confirmedQty);
            }
               
            return good;
        }
    }
}
