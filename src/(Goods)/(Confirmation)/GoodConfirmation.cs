using System;
using MediaPrint;

namespace Warehouse.Core.Goods
{
    public class GoodConfirmation : IGoodConfirmation
    {
        private int _quantity;
        private readonly int _total;

        public GoodConfirmation(IGood good, int totalQuantity)
        {
            Good = good;
            _total = totalQuantity;
        }

        public IGood Good { get; }

        public IConfirmationState State => new ConfirmationState(_quantity, _total);

        public int Increase(int quantity)
        {
            if (_quantity + quantity > _total)
            {
                throw new InvalidOperationException(
                    $"Good confirmation can not be increased " +
                    $"(total:{_total}, actual:{_quantity}, to increase on: {quantity})"
                 );
            }
            _quantity += quantity;
            return _quantity;
        }

        public int Decrease(int quantity)
        {
            if (_quantity < quantity)
            {
                throw new InvalidOperationException(
                    $"Good confirmation can not be decreased " +
                    $"(actual:{_quantity}, to decrease on: {quantity})"
                );
            }
            _quantity -= quantity;
            return _quantity;
        }

        public void Clear()
        {
            _quantity = 0;
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
                .Put("Confirmed", _quantity)
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
