using System.Collections.Generic;

namespace Warehouse.Core
{
    public class WithoutExtraConfirmedGoodDuplicates
    {
        private readonly IList<IGoodConfirmation> _goodConfirmations;

        public WithoutExtraConfirmedGoodDuplicates(IList<IGoodConfirmation> goodConfirmations)
        {
            _goodConfirmations = goodConfirmations;
        }

        public IList<IGoodConfirmation> ToList()
        {
            var extraConfirmedGoods = new List<IReceptionGood>();
            var noDuplicates = new List<IGoodConfirmation>();
            foreach (var confirmation in _goodConfirmations)
            {
                if (confirmation.ConfirmedQuantity > 0)
                {
                    if (extraConfirmedGoods.Contains(confirmation.Good))
                    {
                        continue; // skip because its already been added as Extra confirmed good
                    }
                    if (confirmation.Good.IsExtraConfirmed)
                    {
                        extraConfirmedGoods.Add(confirmation.Good);
                    }

                    noDuplicates.Add(confirmation);
                }
            }
            return noDuplicates;
        }
    }
}
