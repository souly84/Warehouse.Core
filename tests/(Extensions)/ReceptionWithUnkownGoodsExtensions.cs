using System.Threading.Tasks;

namespace Warehouse.Core.Tests.Extensions
{
    public static class ReceptionWithUnkownGoodsExtensions
    {
        public static async Task<ReceptionWithUnkownGoods> ConfirmAsync(
            this ReceptionWithUnkownGoods reception,
            params string[] barcodes)
        {
            foreach (var barcode in barcodes)
            {
                var good = await reception.ByBarcodeAsync(barcode);
                good.Confirmation.Increase(1);
            }
            await reception.Confirmation().CommitAsync();
            return reception;
        }
    }
}
