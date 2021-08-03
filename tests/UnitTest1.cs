using Xunit;

namespace Warehouse.Core.Tests
{
    public class Tests
    {
        [Fact]
        public void Test1()
        {
            Assert.Equal(
                "Hello world",
                new Class1().HelloWorld()
            );
        }
    }
}
