using System;
using MediaPrint;
using Warehouse.Core.Goods;
using Warehouse.Core.Warehouse;

namespace Warehouse.Core
{
    public interface IGood : IPrintable
    {
        IGoodConfirmation Confirmation { get; }

        IStorages Storages { get; }
    }

    public class MockGood : IGood
    {
        private readonly string _id;
        private readonly int _quantity;
        private readonly string _barcode;
        private IGoodConfirmation _confirmation;

        public MockGood(string id, int quantity, string barcode = null)
        {
            _id = id;
            _quantity = quantity;
            _barcode = barcode;
        }

        public IGoodConfirmation Confirmation => _confirmation ?? (_confirmation = new GoodConfirmation(this, _quantity));

        public IStorages Storages => throw new NotImplementedException();

        public override bool Equals(object obj)
        {
            return object.ReferenceEquals(obj, this)
                || TheSameMockObject(obj)
                || TheSameIdOrBarcode(obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_id);
        }

        public void PrintTo(IMedia media)
        {
            media
                .Put("Id", _id)
                .Put("Barcode", _barcode);
        }

        private bool TheSameMockObject(object obj)
        {
            return obj is MockGood good
                && _id == good._id
                && _barcode == good._barcode;
        }

        private bool TheSameIdOrBarcode(object obj)
        {
            return obj is string idOrBarcode
                && (_id == idOrBarcode || _barcode == idOrBarcode);
        }
    }
}
