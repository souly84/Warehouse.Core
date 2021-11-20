using MediaPrint;

namespace Warehouse.Core.Goods
{
    public interface IGoodConfirmation : IPrintable
    {
        IGood Good { get; }

        int Increase(int quantity);

        int Decrease(int quantity);

        bool Done();

        void Clear();
    }
}
