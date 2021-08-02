using NUnit.Framework;

namespace Warehouse.Core.Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            Assert.NotNull(new Class1());
        }
    }
}
