using MediaPrint;

namespace Warehouse.Core
{
    public interface IGood : IPrintable
    {
    }

    public class MockGood : IGood
    {
        private readonly string _id;

        public MockGood(string id)
        {
            _id = id;
        }

        public void PrintTo(IMedia media)
        {
            media.Put("Id", _id);
        }
    }
}
