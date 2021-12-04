using System.Linq;
using System.Threading.Tasks;

namespace Warehouse.Core
{
    public class GoodsConfirmationState : IConfirmationState
    {
        private readonly IEntities<IReceptionGood> _receptionGoods;

        public GoodsConfirmationState(IReception reception)
            : this(reception.Goods)
        {
        }

        public GoodsConfirmationState(IEntities<IReceptionGood> receptionGoods)
        {
            _receptionGoods = receptionGoods;
        }

        public async Task<IConfirmationState.ConfirmationState> ToEnumAsync()
        {
            var goods = await _receptionGoods.ToListAsync();
            if (goods.Any())
            {
                int confirmedCount = 0;
                foreach (var good in goods)
                {
                    var state = await good.Confirmation.State.ToEnumAsync();
                    if (state == IConfirmationState.ConfirmationState.Confirmed)
                    {
                        confirmedCount++;
                    }
                    else if (confirmedCount > 0 || state == IConfirmationState.ConfirmationState.Partially)
                    {
                        return IConfirmationState.ConfirmationState.Partially;
                    }
                }

                if (confirmedCount > 0)
                {
                    return confirmedCount == goods.Count
                       ? IConfirmationState.ConfirmationState.Confirmed
                       : IConfirmationState.ConfirmationState.Partially;
                }
            }

            return IConfirmationState.ConfirmationState.NotStarted;
        }

        public override string ToString()
        {
            return ToEnumAsync().RunSync().ToString();
        }
    }
}
