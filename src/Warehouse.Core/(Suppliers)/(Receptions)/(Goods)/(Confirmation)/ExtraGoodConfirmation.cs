using MediaPrint;

namespace Warehouse.Core
{
    /// <summary>
    /// This confirmation should behave as original good confirmation till the moment it was updated.
    /// After confirmation update it should start behaving as extra confirmed one that never could be
    /// completely confirmed.
    /// </summary>
    public class ExtraGoodConfirmation : IGoodConfirmation
    {
        private readonly IReceptionGood _extraConfirmedGood;
        private readonly IReceptionGood _originalGood;
        private bool _wasUpdated;

        public ExtraGoodConfirmation(
            IReceptionGood extraConfirmedGood,
            IReceptionGood originalGood,
            int confirmedQuantity)
        {
            _extraConfirmedGood = extraConfirmedGood;
            _originalGood = originalGood;
            ConfirmedQuantity = confirmedQuantity;
        }

        public int ConfirmedQuantity { get; private set; }

        public IReceptionGood Good => _extraConfirmedGood;

        public IConfirmationState State => Confirmation().State;

        public void Clear()
        {
            _wasUpdated = true;
            ConfirmedQuantity = 0;
        }

        public int Decrease(int quantity)
        {
            _wasUpdated = true;
            ConfirmedQuantity -= quantity;
            return ConfirmedQuantity;
        }

        public int Increase(int quantity)
        {
            _wasUpdated = true;
            ConfirmedQuantity += quantity;
            return ConfirmedQuantity;
        }

        public override bool Equals(object obj)
        {
            return object.ReferenceEquals(obj, this)
                || Confirmation().Equals(obj);
        }

        public override string ToString()
        {
            return this.ToJson().ToString();
        }

        public override int GetHashCode()
        {
            return Good.GetHashCode();
        }

        public void PrintTo(IMedia media)
        {
            Confirmation().PrintTo(media);
        }

        private IGoodConfirmation Confirmation()
        {
            return _wasUpdated
                ? new GoodConfirmation(_extraConfirmedGood, 1000, ConfirmedQuantity)
                : _originalGood.Confirmation;
        }
    }
}
