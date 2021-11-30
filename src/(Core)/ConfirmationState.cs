using System;
using System.Threading.Tasks;

namespace Warehouse.Core
{
    public class ConfirmationState : IConfirmationState
    {
        private readonly int _confirmedQty;
        private readonly int _totalQty;

        public ConfirmationState(int confirmedQty, int totalQty)
        {
            _confirmedQty = confirmedQty;
            _totalQty = totalQty;
        }

        public override bool Equals(object obj)
        {
            return object.ReferenceEquals(this, obj)
                || (obj is ConfirmationState state
                && _confirmedQty == state._confirmedQty
                && _totalQty == state._totalQty);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_confirmedQty, _totalQty);
        }

        public Task<IConfirmationState.ConfirmationState> ToEnumAsync()
        {
            if (_confirmedQty == _totalQty)
            {
                return Task.FromResult(IConfirmationState.ConfirmationState.Confirmed);
            }

            if (_confirmedQty > 0 && _confirmedQty < _totalQty)
            {
                return Task.FromResult(IConfirmationState.ConfirmationState.Partially);
            }

            return Task.FromResult(IConfirmationState.ConfirmationState.NotStarted);
        }

        public override string ToString()
        {
            return ToEnumAsync().RunSync().ToString();
        }
    }
}
