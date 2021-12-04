using MediaPrint;

namespace Warehouse.Core
{
    public interface ISupplier : IPrintable
    {
        IEntities<IReception> Receptions { get; }
    }

    public class MockSupplier : ISupplier
    {
        public MockSupplier() : this(
            new MockReception(
                new MockReceptionGood("1", 2, "123456789"),
                new MockReceptionGood("2", 2, "123456780"),
                new MockReceptionGood("3", 2, "123456781"),
                new MockReceptionGood("4", 2, "123456782")
            )
        )
        {
        }

        public MockSupplier(params IReception [] receptions)
            : this(new ListOfEntities<IReception>(receptions))
        {
        }

        public MockSupplier(IEntities<IReception> receptions)
        {
            Receptions = receptions;
        }

        public IEntities<IReception> Receptions { get; }

        public void PrintTo(IMedia media)
        {
            media
                .Put("name", "MockSupplier")
                .Put("receptions", Receptions);
        }
    }
}
