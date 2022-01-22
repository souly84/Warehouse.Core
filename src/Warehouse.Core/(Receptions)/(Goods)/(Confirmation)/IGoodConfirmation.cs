using MediaPrint;

namespace Warehouse.Core
{
    public interface IGoodConfirmation : IPrintable
    {
        int ConfirmedQuantity { get; }

        IReceptionGood Good { get; }

        IConfirmationState State { get; }

        int Increase(int quantity);

        int Decrease(int quantity);

        void Clear();
    }
}
