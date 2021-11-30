using MediaPrint;

namespace Warehouse.Core.Goods.Storages
{
    public class StorageGood : IGood
    {
        private readonly IGood _good;
        private readonly int _storageQuantity;

        public StorageGood(IGood good, int storageQuantity)
        {
            _good = good;
            _storageQuantity = storageQuantity;
        }

        public IGoodConfirmation Confirmation => _good.Confirmation;

        public IEntities<IStorage> Storages => _good.Storages;

        public IMovement Movement => _good.Movement;

        public int Quantity => _good.Quantity;

        public void PrintTo(IMedia media)
        {
            _good.PrintTo(media);
            media.Put("StorageQty", _storageQuantity);
        }
    }
}
