using MediaPrint;

namespace Warehouse.Core.Receptions
{
    public class ConfirmedGood : IGood
    {
        private readonly IGood _good;
        private int _quantity;

        public ConfirmedGood(IGood good)
        {
            _good = good;
        }

        public int Increase()
        {
            _quantity++;
            return _quantity;
        }

        public int Decrease()
        {
            _quantity--;
            return _quantity;
        }

        public override int GetHashCode()
        {
            return _good.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return _good.Equals(obj);
        }

        public void PrintTo(IMedia media)
        {
            _good.PrintTo(media);
            media.Put("ConfirmedQty", _quantity);
        }
    }
}
