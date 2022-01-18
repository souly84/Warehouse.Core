using MediaPrint;

namespace Warehouse.Core
{
    public class ExtraConfirmedReceptionGood : IReceptionGood
    {
        private readonly IReceptionGood _good;
        private readonly int _defaultMaxQuantity;
        private IGoodConfirmation? _confirmation;

        public ExtraConfirmedReceptionGood(IReceptionGood good, int defaultMaxQuantity)
        {
            _good = good;
            _defaultMaxQuantity = defaultMaxQuantity;
        }

        public int Quantity => _good.Quantity;

        public IGoodConfirmation Confirmation => _confirmation ?? (_confirmation = new GoodConfirmation(this, _defaultMaxQuantity));

        public override bool Equals(object? obj)
        {
            return _good.Equals(obj);
        }

        public override int GetHashCode()
        {
            return _good.GetHashCode();
        }

        public void PrintTo(IMedia media)
        {
            _good.PrintTo(media);
        }
    }
}
