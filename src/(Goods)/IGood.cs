using System;
using MediaPrint;
using Warehouse.Core.Goods;

namespace Warehouse.Core
{
    public interface IGood : IPrintable
    {
        int Quantity { get; }

        IGoodConfirmation Confirmation { get; }

        IEntities<IStorage> Storages { get; }

        IMovement Movement { get; }
    }

    public class MockGood : IGood
    {
        private readonly string _id;
        private readonly string _barcode;
        private IGoodConfirmation _confirmation;
        private IEntities<IStorage> _storages;

        public MockGood(
            string id,
            int quantity,
            string barcode = null)
        {
            _id = id;
            Quantity = quantity;
            _barcode = barcode;
        }

        public IGoodConfirmation Confirmation => _confirmation ?? (_confirmation = new GoodConfirmation(this, Quantity));

        public IEntities<IStorage> Storages => _storages ?? (_storages = new ListOfEntities<IStorage>(new MockStorage(this)));

        public IMovement Movement => new StotageMovement(this);

        public int Quantity { get; }

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
                .Put("Quantity", Quantity)
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
