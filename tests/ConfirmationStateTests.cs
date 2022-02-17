using System.Threading.Tasks;
using Xunit;

namespace Warehouse.Core.Tests
{
    public class ConfirmationStateTests
    {
        [Fact]
        public async Task DoneWhenExtraConfirmed()
        {
            Assert.Equal(
                IConfirmationState.ConfirmationState.Confirmed,
                await new ConfirmationState(6, 5).ToEnumAsync()
            );
        }
    }
}
