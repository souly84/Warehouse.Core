using MediaPrint;

namespace Warehouse.Core
{
    public interface IReceptionGood : IPrintable
    {
        int Quantity { get; }

        IGoodConfirmation Confirmation { get; }
    }

    public class MockReceptionGood : IReceptionGood
    {
        private readonly string _id;
        private readonly string? _barcode;
        private IGoodConfirmation? _confirmation;

        public MockReceptionGood(
            string id,
            int quantity,
            string? barcode = null)
        {
            _id = id;
            Quantity = quantity;
            _barcode = barcode;
        }

        public IGoodConfirmation Confirmation => _confirmation ?? (_confirmation = new GoodConfirmation(this, Quantity));

        public int Quantity { get; }

        public override bool Equals(object obj)
        {
            return object.ReferenceEquals(obj, this)
                || TheSameMockObject(obj)
                || TheSameIdOrBarcode(obj);
        }

        public override int GetHashCode()
        {
            return _id.GetHashCode();
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
            return obj is MockReceptionGood good
                && _id == good._id
                && _barcode == good._barcode;
        }

        private bool TheSameIdOrBarcode(object obj)
        {
            return obj is string idOrBarcode
                && (_id.ToString() == idOrBarcode || _barcode == idOrBarcode);
        }
    }
}
