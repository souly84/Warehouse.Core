using MediaPrint;
using Warehouse.Core.Receptions.Goods;

namespace Warehouse.Core.Goods
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
