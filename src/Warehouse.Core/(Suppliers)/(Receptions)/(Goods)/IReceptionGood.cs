using MediaPrint;

namespace Warehouse.Core
{
    public interface IReceptionGood : IPrintable
    {
        string Id { get; }
        
        int Quantity { get; }

        bool IsUnknown { get; }

        bool IsExtraConfirmed { get; }

        IGoodConfirmation Confirmation { get; }
    }

    public class MockReceptionGood : IReceptionGood
    {
        private readonly string _id;
        private readonly string? _barcode;
        private readonly int _confirmedQuantity;
        private IGoodConfirmation? _confirmation;

        public MockReceptionGood(
            string id,
            int quantity,
            string? barcode = null,
            bool isUnknown = false,
            int confirmedQuantity = 0)
        {
            _id = id;
            Quantity = quantity;
            _barcode = barcode;
            IsUnknown = isUnknown;
            IsExtraConfirmed = confirmedQuantity > Quantity;
            _confirmedQuantity = confirmedQuantity;
        }

        public string Id => _id;

        public IGoodConfirmation Confirmation => _confirmation ?? (_confirmation = new GoodConfirmation(this, Quantity, _confirmedQuantity));

        public int Quantity { get; }

        public bool IsUnknown { get; }

        public bool IsExtraConfirmed { get; }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj)
                || TheSameMockObject(obj)
                || TheSameGoodObject(obj)
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
                .Put("IsUnknown", IsUnknown)
                .Put("IsExtraConfirmed", IsExtraConfirmed)
                .Put("Barcode", _barcode);
        }

        private bool TheSameMockObject(object obj)
        {
            return obj is MockReceptionGood good
                && _id == good._id
                && _barcode == good._barcode
                && IsUnknown == good.IsUnknown
                && IsExtraConfirmed == good.IsExtraConfirmed;
        }

        private bool TheSameGoodObject(object obj)
        {
            return obj is IReceptionGood good && _id == good.Id;
        }

        private bool TheSameIdOrBarcode(object obj)
        {
            return obj is string idOrBarcode
                && (_id.ToString() == idOrBarcode || _barcode == idOrBarcode);
        }
    }
}
