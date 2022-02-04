using System;
using MediaPrint;

namespace Warehouse.Core
{
    public class GoodConfirmation : IGoodConfirmation
    {
        private readonly int _total;

        public GoodConfirmation(IReceptionGood good, int totalQuantity)
            : this(good, totalQuantity, 0)
        {
        }

        public GoodConfirmation(IReceptionGood good, int totalQuantity, int confirmedQuantity)
        {
            Good = good;
            _total = totalQuantity;
            ConfirmedQuantity = confirmedQuantity;
        }

        public IReceptionGood Good { get; }

        public IConfirmationState State => new ConfirmationState(ConfirmedQuantity, _total);

        public int ConfirmedQuantity { get; private set; }

        public int Increase(int quantity)
        {
            if (ConfirmedQuantity + quantity > _total)
            {
                throw new InvalidOperationException(
                    $"Good confirmation can not be increased " +
                    $"(total:{_total}, actual:{ConfirmedQuantity}, to increase on: {quantity})"
                 );
            }
            ConfirmedQuantity += quantity;
            return ConfirmedQuantity;
        }

        public int Decrease(int quantity)
        {
            if (ConfirmedQuantity < quantity)
            {
                throw new InvalidOperationException(
                    $"Good confirmation can not be decreased " +
                    $"(actual:{ConfirmedQuantity}, to decrease on: {quantity})"
                );
            }
            ConfirmedQuantity -= quantity;
            return ConfirmedQuantity;
        }

        public void Clear()
        {
            ConfirmedQuantity = 0;
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
                .Put("Confirmed", ConfirmedQuantity)
                .Put("State", State);
        }

        private bool TheSameConfirmation(object obj)
        {
            return obj is IGoodConfirmation goodConfirmation
                && goodConfirmation.Good.Equals(Good)
                && goodConfirmation.State.Equals(State)
                && goodConfirmation.ConfirmedQuantity == ConfirmedQuantity;
        }
    }
}
