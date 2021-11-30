using MediaPrint;

namespace Warehouse.Core.Goods
{
    public interface IGoodConfirmation : IPrintable
    {
        IGood Good { get; }

        IConfirmationState State { get; }

        int Increase(int quantity);

        int Decrease(int quantity);

        void Clear();
    }
}
