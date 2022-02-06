using System.Threading.Tasks;

namespace Warehouse.Core.Tests.Extensions
{
    public static class ReceptionExtensions
    {
        public static async Task<IReception> ConfirmAsync(
            this IReception reception,
            params string[] barcodes)
        {
            await reception.ConfirmWithoutCommitAsync(barcodes);
            await reception.Confirmation().CommitAsync();
            return reception;
        }

        public static async Task<IReception> ConfirmWithoutCommitAsync(
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
            return reception;
        }
    }
}
