using System.Threading.Tasks;
using Warehouse.Core.Tests.Extensions;
using Xunit;

namespace Warehouse.Core.Tests
{
    public class ReceptionConfirmationTests
    {
        [Fact]
        public async Task ReceptionValidation()
        {
            Assert.Equal(
                new ConfirmedGoods(
                    new MockGood("good1", 4),
                    new MockGood("good2", 8)
                ).ToList(),
                (await new ConfirmedReception<MockReception>(
                    new MockReception(
                        new MockGood("good1", 4),
                        new MockGood("good2", 8)
                    )
                ).ConfirmAsync()).ValidatedGoods
            );  
        }
    }
}
