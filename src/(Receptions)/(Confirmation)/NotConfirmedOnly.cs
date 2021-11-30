using System.Collections.Generic;
using System.Threading.Tasks;
using Warehouse.Core.Goods;

namespace Warehouse.Core.Receptions
{
    public class NotConfirmedOnly : Confirmation
    {
        private readonly IConfirmation _confirmation;

        public NotConfirmedOnly(IConfirmation confirmation)
            : base(confirmation)
        {
            _confirmation = confirmation;
        }

        public override async Task<List<IGoodConfirmation>> ToListAsync()
        {
            var notConfirmedOnly = new List<IGoodConfirmation>();
            var goodConfirmations = await _confirmation.ToListAsync();
            foreach (var goodConfirmation in goodConfirmations)
            {
                if (!await goodConfirmation.DoneAsync())
                {
                    notConfirmedOnly.Add(goodConfirmation);
                }
            }
            return notConfirmedOnly;
        }
    }
}
