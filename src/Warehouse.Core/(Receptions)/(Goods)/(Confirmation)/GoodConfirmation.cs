using System;
using MediaPrint;

namespace Warehouse.Core
{
    public class GoodConfirmation : IGoodConfirmation
    {
        private int _confirmedQuantity;
        private readonly int _total;

        public GoodConfirmation(IReceptionGood good, int totalQuantity)
            : this(good, totalQuantity, 0)
        {
        }

        public GoodConfirmation(IReceptionGood good, int totalQuantity, int confirmedQuantity)
        {
            Good = good;
            _total = totalQuantity;
            _confirmedQuantity = confirmedQuantity;
        }

        public IReceptionGood Good { get; }

        public IConfirmationState State => new ConfirmationState(_confirmedQuantity, _total);

        public int Increase(int quantity)
        {
            if (_confirmedQuantity + quantity > _total)
            {
                throw new InvalidOperationException(
                    $"Good confirmation can not be increased " +
                    $"(total:{_total}, actual:{_confirmedQuantity}, to increase on: {quantity})"
                 );
            }
            _confirmedQuantity += quantity;
            return _confirmedQuantity;
        }

        public int Decrease(int quantity)
        {
            if (_confirmedQuantity < quantity)
            {
                throw new InvalidOperationException(
                    $"Good confirmation can not be decreased " +
                    $"(actual:{_confirmedQuantity}, to decrease on: {quantity})"
                );
            }
            _confirmedQuantity -= quantity;
            return _confirmedQuantity;
        }

        public void Clear()
        {
            _confirmedQuantity = 0;
        }

        public override bool Equals(object obj)
        {
            return object.ReferenceEquals(obj, this)
                || TheSameConfirmation(obj);
        }

        public override int GetHashCode()
        {
            return Good.GetHashCode();
        }

        public override string ToString()
        {
            return this.ToJson().ToString();
        }

        public void PrintTo(IMedia media)
        {
            media
                .Put("Good", Good)
                .Put("Total", _total)
                .Put("Confirmed", _confirmedQuantity)
                .Put("State", State);
        }

        private bool TheSameConfirmation(object obj)
        {
            return obj is IGoodConfirmation goodConfirmation
                && goodConfirmation.Good.Equals(Good)
                && goodConfirmation.State.Equals(State);
        }
    }
}
