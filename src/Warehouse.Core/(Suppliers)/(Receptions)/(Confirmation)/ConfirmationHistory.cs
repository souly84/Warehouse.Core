using System.Collections.Generic;
using System.Threading.Tasks;

namespace Warehouse.Core
{
    public class ConfirmationHistory : Confirmation
    {
        private readonly IConfirmation _confirmation;

        public ConfirmationHistory(IConfirmation confirmation)
            : base(confirmation)
        {
            _confirmation = confirmation;
        }

        public override async Task<List<IGoodConfirmation>> ToListAsync()
        {
            var confirmedOnly = new List<IGoodConfirmation>();
            var goodConfirmations = await _confirmation.ToListAsync();
            foreach (var goodConfirmation in goodConfirmations)
            {
                if (await goodConfirmation.DoneAsync()
                    || goodConfirmation.Good.IsExtraConfirmed
                    || goodConfirmation.Good.IsUnknown)
                {
                    confirmedOnly.Add(goodConfirmation);
                }
            }
            return confirmedOnly;
        }
    }
}
