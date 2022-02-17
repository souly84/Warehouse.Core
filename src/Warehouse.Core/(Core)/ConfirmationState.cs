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
                || (obj is ConfirmationState state && ToEnum() == state.ToEnum());
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_confirmedQty, _totalQty);
        }

        public Task<IConfirmationState.ConfirmationState> ToEnumAsync()
        {
            return Task.FromResult(ToEnum());
        }

        public override string ToString()
        {
            return ToEnum().ToString();
        }

        private IConfirmationState.ConfirmationState ToEnum()
        {
            if (_confirmedQty >= _totalQty)
            {
                return IConfirmationState.ConfirmationState.Confirmed;
            }

            if (_confirmedQty > 0 && _confirmedQty < _totalQty)
            {
                return IConfirmationState.ConfirmationState.Partially;
            }

            return IConfirmationState.ConfirmationState.NotStarted;
        }
    }
}
