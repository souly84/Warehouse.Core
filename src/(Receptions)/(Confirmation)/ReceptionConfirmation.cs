using System.Collections.Generic;
using System.Threading.Tasks;
using Warehouse.Core.Goods;

namespace Warehouse.Core.Receptions
{
    public class ReceptionConfirmation : IConfirmation
    {
        private readonly IReception _reception;

        private List<ConfirmedGood> _validatedGoods;

        public ReceptionConfirmation(IReception reception)
            : this(reception, new List<ConfirmedGood>())
        {
        }

        public ReceptionConfirmation(IReception reception, List<ConfirmedGood> scannedGoods)
        {
            _reception = reception;
            _validatedGoods = scannedGoods;
        }

        public IGoods Goods => new ListOfGoods(new List<IGood>(_validatedGoods));

        public Task AddAsync(IGood good)
        {
            var validatedGood = new ConfirmedGood(good);
            var indexOfGood = _validatedGoods.IndexOf(validatedGood);
            if (indexOfGood == -1)
            {
                _validatedGoods.Add(validatedGood);
                indexOfGood = _validatedGoods.Count - 1;
            }
            _validatedGoods[indexOfGood].Increase();
            return Task.CompletedTask;
        }

        public Task RemoveAsync(IGood good)
        {
            var indexOfGood = _validatedGoods.IndexOf(new ConfirmedGood(good));
            if (indexOfGood != -1)
            {
                var decreasedQuantity = _validatedGoods[indexOfGood].Decrease();
                if (decreasedQuantity == 0)
                {
                    _validatedGoods.RemoveAt(indexOfGood);
                }
            }
            return Task.CompletedTask;
        }

        public Task CommitAsync()
        {
            return _reception.ValidateAsync(Goods);
        }
    }
}
