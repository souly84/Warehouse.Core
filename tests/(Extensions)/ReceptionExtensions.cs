using System.Threading.Tasks;

namespace Warehouse.Core.Tests.Extensions
{
    public static class ReceptionExtensions
    {
        public static async Task<IReception> ConfirmAsync(
            this IReception reception,
            params string[] barcodes)
        {
            foreach (var barcode in barcodes)
            {
                var goods = await reception.ByBarcodeAsync(barcode);
                foreach (var good in goods)
                {
                    good.Confirmation.Increase(1);
                }
            }
            await reception.Confirmation().CommitAsync();
            return reception;
        }
    }
}
