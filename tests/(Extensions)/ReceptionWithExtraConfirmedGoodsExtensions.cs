﻿using System.Threading.Tasks;

namespace Warehouse.Core.Tests
{
    public static class ReceptionWithExtraConfirmedGoodsExtensions
    {
        public static async Task<ReceptionWithExtraConfirmedGoods> ConfirmAsync(
            this ReceptionWithExtraConfirmedGoods reception,
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
