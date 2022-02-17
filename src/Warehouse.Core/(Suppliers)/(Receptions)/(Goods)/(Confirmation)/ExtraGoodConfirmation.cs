namespace Warehouse.Core
{
    public class ExtraGoodConfirmation : IGoodConfirmation
    {
        public ExtraGoodConfirmation(
            IReceptionGood good,
            int totalQuantity,
            int confirmedQuantity)
        {
        }
    }
}
