namespace Warehouse.Core.Tests.Extensions
{
    public static class GoodExtensions
    {
        public static IGood FullyConfirmed(this IGood good)
        {
            while (!good.Confirmed())
            {
                good.Confirmation.Increase(1);
            }
            return good;
        }
    }
}
