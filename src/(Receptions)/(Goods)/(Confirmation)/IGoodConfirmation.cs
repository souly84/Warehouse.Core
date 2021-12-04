using MediaPrint;

namespace Warehouse.Core
{
    public interface IGoodConfirmation : IPrintable
    {
        IReceptionGood Good { get; }

        IConfirmationState State { get; }

        int Increase(int quantity);

        int Decrease(int quantity);

        void Clear();
    }
}
