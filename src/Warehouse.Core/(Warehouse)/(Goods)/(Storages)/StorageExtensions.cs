using System.Threading.Tasks;

namespace Warehouse.Core
{
    public static class StorageExtensions
    {
        public static async Task<int> QuantityForAsync(this IStorage storage, string goodBarcode)
        {
            var goods = await storage.Goods.For(goodBarcode).ToListAsync();
            var total = 0;
            foreach (var good in goods)
            {
                total += await storage.QuantityForAsync(good);
            }
            return total;
        }
    }
}
