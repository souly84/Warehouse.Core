using System;
using System.Collections.Generic;
using System.Linq;
using MediaPrint;

namespace Warehouse.Core
{
    /// <summary>
    /// This class is used when extra good was scanned during reception validation operation.
    /// </summary>
    public class ExtraConfirmedReceptionGood : IReceptionGood
    {
        private readonly IList<IReceptionGood> _goods;
        private readonly int _defaultMaxQuantity;
        private IGoodConfirmation? _confirmation;

        public ExtraConfirmedReceptionGood(params IReceptionGood[] goods)
            : this(new List<IReceptionGood>(goods))
        {
        }

        public ExtraConfirmedReceptionGood(IList<IReceptionGood> goods)
            : this(goods, 1000)
        {
        }

        public ExtraConfirmedReceptionGood(IList<IReceptionGood> goods, int defaultMaxQuantity)
        {
            _goods = goods;
            _defaultMaxQuantity = defaultMaxQuantity;
        }

        public int Quantity => _goods.Sum(g => g.Quantity);

        public IGoodConfirmation Confirmation => _confirmation ?? (_confirmation = new GoodConfirmation(
            this,
            _defaultMaxQuantity,
            GoodsConfirmed())
        );

        public bool IsUnknown => false;

        public bool IsExtraConfirmed => true;

        public override bool Equals(object? obj)
        {
            return object.ReferenceEquals(this, obj)
                || (obj is ExtraConfirmedReceptionGood extraConfirmedReceptionGood && _goods.AtLeastOne(extraConfirmedReceptionGood._goods))
                || _goods.Contains(obj);
        }

        public override int GetHashCode()
        {
            var hasCode = new HashCode();
            foreach (var good in _goods)
            {
                hasCode.Add(good);
            }
            return hasCode.ToHashCode();
        }

        public void PrintTo(IMedia media)
        {
            _goods.First().PrintTo(media);
        }

        private int GoodsConfirmed()
        {
            return _goods.Sum(g => g.Confirmation.ConfirmedQuantity);
        }
    }
}
