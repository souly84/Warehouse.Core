using System.Collections.Generic;
using System.Threading.Tasks;
using Warehouse.Core.Goods;

namespace Warehouse.Core.Receptions
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
                if (await goodConfirmation.DoneAsync())
                {
                    confirmedOnly.Add(goodConfirmation);
                }
            }
            return confirmedOnly;
        }
    }
}
