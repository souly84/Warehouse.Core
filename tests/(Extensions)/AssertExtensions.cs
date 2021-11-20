using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Xunit.Abstractions;

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

        public static void EqualJson(string expectedJson, string actualJson, ITestOutputHelper output = null)
        {
            JObject expected = JObject.Parse(expectedJson);
            JObject actual = JObject.Parse(actualJson);
            if (output != null)
            {
                output.WriteLine("Expected:" + expectedJson);
                output.WriteLine("Actual:" + actualJson);
            }

            Xunit.Assert.Equal(expected, actual, JToken.EqualityComparer);
        }
    }
}
