using System;
using System.Collections.Generic;
using MediaPrint;

namespace Warehouse.Core.Goods
{
    public class GoodConfirmation : IGoodConfirmation
    {
        private int _quantity;
        private int _total;

        public GoodConfirmation(IGood good, int totalQuantity)
        {
            Good = good;
            _total = totalQuantity;
        }

        public IGood Good { get; }

        public void Clear()
        {
            _quantity = 0;
        }

        public int Decrease(int quantity)
        {
            if (_quantity < quantity)
            {
                throw new InvalidOperationException(
                    $"Good confirmation can not be decreased (actual:{_quantity}, to decrease: {quantity})"
                );
            }
            _quantity -= quantity;
            return _quantity;
        }

        public bool Done()
        {
            return _quantity == _total;
        }

        public override bool Equals(object obj)
        {
            return obj is GoodConfirmation confirmation &&
                   _quantity == confirmation._quantity &&
                   _total == confirmation._total &&
                   EqualityComparer<IGood>.Default.Equals(Good, confirmation.Good);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_quantity, _total, Good);
        }

        public int Increase(int quantity)
        {
            if (_quantity + quantity> _total)
            {
                throw new InvalidOperationException(
                    $"Good confirmation can not be increased (total:{_total}, actual:{_quantity}, to increase: {quantity})"
                 );
            }
            _quantity += quantity;
            return _quantity;
        }

        public void PrintTo(IMedia media)
        {
            media
                .Put("Good", Good)
                .Put("Total", _total)
                .Put("Confirmed", _quantity);
        }
    }
}
