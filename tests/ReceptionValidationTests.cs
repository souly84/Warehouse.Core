using System.Threading.Tasks;
using Warehouse.Core.Goods;
using Warehouse.Core.Tests.Extensions;
using Xunit;

namespace Warehouse.Core.Tests
{
    public class ReceptionConfirmationTests
    {
        [Fact]
        public async Task ReceptionValidation()
        {
            var validatedReception = await new ConfirmedReception<MockReception>(
                new MockReception(
                    new MockGood("good1"),
                    new MockGood("good2")
                )
            ).ValidateAsync();

            await Assert.EqualAsync(
                new ListOfGoods(
                    new MockGood("good1"),
                    new MockGood("good1")
                ),
                new ListOfGoods(validatedReception.ValidatedGoods)
            );  
        }
    }
}
