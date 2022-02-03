using System;
using MediaPrint;
using Warehouse.Core.Plugins;

namespace Warehouse.Core
{
    public class StatefulGoodConfirmation : IGoodConfirmation
    {
        private readonly IGoodConfirmation _origin;
        private readonly IKeyValueStore _goodState;

        public StatefulGoodConfirmation(IGoodConfirmation origin, IKeyValueStore goodState)
        {
            _origin = origin;
            _goodState = goodState;
        }

        public int ConfirmedQuantity => _origin.ConfirmedQuantity;

        public IReceptionGood Good => _origin.Good;

        public IConfirmationState State => _origin.State;

        private string GoodUniqueKey => _origin.Good.Id;

        public void Clear()
        {
            _goodState.Remove(GoodUniqueKey);
            _origin.Clear();
        }

        public int Decrease(int quantity)
        {
            _goodState.Set(
                GoodUniqueKey,
                (Convert.ToInt32(_goodState.Get(GoodUniqueKey)) - 1).ToString()
            );
            return _origin.Decrease(quantity);
        }

        public int Increase(int quantity)
        {
            _goodState.Set(
                GoodUniqueKey,
                (Convert.ToInt32(_goodState.Get(GoodUniqueKey)) + 1).ToString()
            );
            return _origin.Increase(quantity);
        }

        public void PrintTo(IMedia media)
        {
            _origin.PrintTo(media);
        }
    }
}
