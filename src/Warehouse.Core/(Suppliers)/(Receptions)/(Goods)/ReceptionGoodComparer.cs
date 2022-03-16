using System.Collections.Generic;

namespace Warehouse.Core
{
    public class ReceptionGoodComparer : IComparer<IGoodConfirmation>, IComparer<IReceptionGood>
    {
        public int Compare(IGoodConfirmation x, IGoodConfirmation y)
        {
            if (x == null)
            {
                return y == null ? 0 : -1;
            }
            else
            {
                return y != null
                    ? Compare(x.Good, y.Good)
                    : 1;
            }
        }

        public int Compare(IReceptionGood x, IReceptionGood y)
        {
            if (x == null)
            {
                return y == null ? 0 : -1;
            }
            return y != null
                ? CompareUnknownAndExtraConfirmed(x, y)
                : 1;
        }

        private int CompareUnknownAndExtraConfirmed(IReceptionGood x, IReceptionGood y)
        {
            if (x.IsUnknown)
            {
                return y.IsUnknown ? 0 : -1;
            }

            if (x.IsExtraConfirmed)
            {
                return y.IsExtraConfirmed ? 0 : -1;
            }

            return (y.IsExtraConfirmed || y.IsUnknown) ? 1 : 0;
        }
    }
}
