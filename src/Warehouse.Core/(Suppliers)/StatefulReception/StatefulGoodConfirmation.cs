using MediaPrint;
using Warehouse.Core.Plugins;

namespace Warehouse.Core
{
    public class StatefulGoodConfirmation : IGoodConfirmation
    {
        private readonly IGoodConfirmation _origin;
        private readonly IKeyValueStorage _goodState;
        private readonly string _goodUniqueKey;

        public StatefulGoodConfirmation(
            IGoodConfirmation origin,
            IKeyValueStorage goodState,
            string goodUniqueKey)
        {
            _origin = origin;
            _goodState = goodState;
            _goodUniqueKey = goodUniqueKey;
        }

        public int ConfirmedQuantity => _origin.ConfirmedQuantity;

        public IReceptionGood Good => _origin.Good;

        public IConfirmationState State => _origin.State;

        public void Clear()
        {
            _goodState.Remove(_goodUniqueKey);
            _origin.Clear();
        }

        public int Decrease(int quantity)
        {
            _goodState.Set(
                _goodUniqueKey,
                _goodState.Get<int>(_goodUniqueKey) - 1
            );
            return _origin.Decrease(quantity);
        }

        public override bool Equals(object? obj)
        {
            return ReferenceEquals(this, obj)
                || _origin.Equals(obj);
        }

        public override int GetHashCode()
        {
            return _origin.GetHashCode();
        }

        public int Increase(int quantity)
        {
            if (Good.IsExtraConfirmed && _goodState.Contains(Good.Id))
            {
                // Usually it happens when extra confirmed good appears
                _goodState.Set(
                   _goodUniqueKey,
                   _goodState.Get<int>(_goodUniqueKey) + _goodState.Get<int>(Good.Id)
                );
                _goodState.Remove(Good.Id);
            }

            _goodState.Set(
                _goodUniqueKey,
                _goodState.Get<int>(_goodUniqueKey) + 1
            );
            return _origin.Increase(quantity);
        }

        public void PrintTo(IMedia media)
        {
            _origin.PrintTo(media);
        }

        public override string ToString()
        {
            return this.ToJson().ToString();
        }
    }
}
