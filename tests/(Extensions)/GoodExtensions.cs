using System.Threading.Tasks;

namespace Warehouse.Core.Tests.Extensions
{
    public static class GoodExtensions
    {
        public static async Task<IGood> FullyConfirmed(this IGood good)
        {
            while (!await good.ConfirmedAsync())
            {
                good.Confirmation.Increase(1);
            }
            return good;
        }

        public static async Task<IGood> PartiallyConfirmed(this IGood good, int confirmedQty)
        {
            if (!await good.ConfirmedAsync())
            {
                good.Confirmation.Increase(confirmedQty);
            }
               
            return good;
        }
    }
}
