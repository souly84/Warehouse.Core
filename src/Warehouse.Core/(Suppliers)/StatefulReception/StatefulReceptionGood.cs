using MediaPrint;
using Warehouse.Core.Plugins;

namespace Warehouse.Core
{
    public class StatefulReceptionGood : IReceptionGood
    {
        private readonly IReceptionGood _origin;
        private readonly IKeyValueStorage _goodState;
        private readonly string _barcodeData;

        public StatefulReceptionGood(
            IReceptionGood origin,
            IKeyValueStorage goodState,
            string barcodeData)
        {
            _origin = origin;
            _goodState = goodState;
            _barcodeData = barcodeData;
        }

        public int Quantity => _origin.Quantity;

        public bool IsUnknown => _origin.IsUnknown;

        public bool IsExtraConfirmed => _origin.IsExtraConfirmed;

        public IGoodConfirmation Confirmation => new StatefulGoodConfirmation(
            _origin.Confirmation,
            _goodState,
            Id
        );

        public string Id => _origin.IsUnknown || _origin.IsExtraConfirmed
            ? _barcodeData
            : _origin.Id;

        public override bool Equals(object? obj)
        {
            return ReferenceEquals(this, obj)
                || (obj is StatefulReceptionGood receptionGood && receptionGood._origin.Equals(_origin))
                || _origin.Equals(obj);
        }

        public override int GetHashCode()
        {
            return _origin.GetHashCode();
        }

        public void PrintTo(IMedia media)
        {
            _origin.PrintTo(media);
        }
    }
}
