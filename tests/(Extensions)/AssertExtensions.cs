using System.Threading.Tasks;

namespace Warehouse.Core.Tests
{
    public class Assert : Xunit.Assert
    {
        public static async Task EqualAsync(IGoods expected, IGoods actual)
        {
            var expectedList = await expected.ToListAsync();
            var actualList = await actual.ToListAsync();

            Xunit.Assert.Equal(expectedList, actualList);
        }
    }
}
