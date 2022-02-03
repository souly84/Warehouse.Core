using MediaPrint;
using Warehouse.Core.Plugins;

namespace Warehouse.Core
{
    public class StatefulReceptionGood : IReceptionGood
    {
        private readonly IReceptionGood _origin;
        private readonly IKeyValueStore _goodState;

        public StatefulReceptionGood(
            IReceptionGood origin,
            IKeyValueStore goodState)
        {
            _origin = origin;
            _goodState = goodState;
        }

        public int Quantity => _origin.Quantity;

        public bool IsUnknown => _origin.IsUnknown;

        public bool IsExtraConfirmed => _origin.IsExtraConfirmed;

        public IGoodConfirmation Confirmation => new StatefulGoodConfirmation(
            _origin.Confirmation,
            _goodState
        );

        public string Id => _origin.Id;

        public void PrintTo(IMedia media)
        {
            _origin.PrintTo(media);
        }
    }
}
