using System;
using MediaPrint;
using Warehouse.Core.Goods;

namespace Warehouse.Core
{
    public interface IGood : IPrintable
    {
        IGoodConfirmation Confirmation { get; }
    }

    public class MockGood : IGood
    {
        private readonly string _id;
        private readonly int _quantity;
        private IGoodConfirmation _confirmation;

        public MockGood(string id, int quantity)
        {
            _id = id;
            _quantity = quantity;
        }

        public IGoodConfirmation Confirmation => _confirmation ?? (_confirmation = new GoodConfirmation(this, _quantity));

        public override bool Equals(object obj)
        {
            return obj is MockGood good
                && _id == good._id
                && _quantity == good._quantity;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_id);
        }

        public void PrintTo(IMedia media)
        {
            media.Put("Id", _id);
        }
    }
}
