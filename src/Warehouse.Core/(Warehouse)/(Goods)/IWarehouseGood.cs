using System;
using MediaPrint;

namespace Warehouse.Core
{
    public interface IWarehouseGood : IPrintable
    {
        int Quantity { get;}

        IStorages Storages { get; }

        IMovement Movement { get; }
    }

    public class MockWarehouseGood : IWarehouseGood
    {
        private readonly string _id;
        private readonly string? _barcode;
        private IStorages? _storages;

        public MockWarehouseGood(
            string id,
            int quantity,
            string? barcode = null)
        {
            _id = id;
            Quantity = quantity;
            _barcode = barcode;
        }


        public IStorages Storages => _storages ?? (_storages = new MockStorages(
            new ListOfEntities<IStorage>(new MockStorage()),
            new ListOfEntities<IStorage>(new MockStorage()),
            new ListOfEntities<IStorage>(new MockStorage(this))
        ));

        public IMovement Movement => new StockMovement(this);

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
            return obj is MockWarehouseGood good
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
