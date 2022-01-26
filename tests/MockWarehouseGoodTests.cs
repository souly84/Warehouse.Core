using System.Threading.Tasks;
using Xunit;

namespace Warehouse.Core.Tests
{
    public class MockWarehouseGoodTests
    {
        [Fact]
        public async Task WithStogares()
        {
            var good = new MockWarehouseGood("1", 1);
            Assert.Equal(
                await new MockStorages(
                    new ListOfEntities<IStorage>(
                        new MockStorage(good),
                        new MockStorage(good),
                        new MockStorage(good)
                    ),
                    new ListOfEntities<IStorage>(
                        new MockStorage(good)
                    ),
                    new ListOfEntities<IStorage>(
                        new MockStorage(good)
                    )
                ).ToListAsync(),
                await good.With(
                    new MockStorages(
                        new ListOfEntities<IStorage>(
                            new MockStorage(good),
                            new MockStorage(good),
                            new MockStorage(good)
                        ),
                        new ListOfEntities<IStorage>(
                            new MockStorage(good)
                        ),
                        new ListOfEntities<IStorage>(
                            new MockStorage(good)
                        )
                    )
                ).Storages.ToListAsync()
            );
        }
    }
}
