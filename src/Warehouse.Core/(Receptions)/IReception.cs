using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediaPrint;

namespace Warehouse.Core
{
    public interface IReception
    {
        IReceptionGoods Goods { get; }

        Task ValidateAsync(IList<IGoodConfirmation> goodsToValidate);
    }

    public class MockReception : IReception, IPrintable
    {
        private readonly DateTime _receptionDate;

        public MockReception(params IReceptionGood[] goods)
            : this(DateTime.Now, goods)
        {
        }

        public MockReception(DateTime receptionDate, params IReceptionGood[] goods)
            : this(receptionDate, new ListOfEntities<IReceptionGood>(goods))
        {
        }

        public MockReception(DateTime receptionDate, IEntities<IReceptionGood> goods)
            : this(receptionDate, new MockReceptionGoods(goods))
        {
        }

        public MockReception(DateTime receptionDate, IReceptionGoods goods)
        {
            _receptionDate = receptionDate;
            Goods = goods;
        }

        public IReceptionGoods Goods { get; }

        public List<IGoodConfirmation> ValidatedGoods { get; } = new List<IGoodConfirmation>();

        public override bool Equals(object? obj)
        {
            return ReferenceEquals(this, obj)
                || (obj is MockReception reception && _receptionDate == reception._receptionDate)
                || (obj is DateTime receptionDate && receptionDate.Date == _receptionDate.Date);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_receptionDate, Goods);
        }

        public void PrintTo(IMedia media)
        {
            media
                .Put("ReceptionDate", _receptionDate)
                .Put("Goods", Goods.ToListAsync().RunSync());
        }

        public Task ValidateAsync(IList<IGoodConfirmation> goodsToValidate)
        {
            ValidatedGoods.AddRange(goodsToValidate);
            return Task.CompletedTask;
        }
    }
}
