using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediaPrint;

namespace Warehouse.Core
{
    public interface IReception
    {
        string Id { get; }

        IReceptionGoods Goods { get; }

        Task ValidateAsync(IList<IGoodConfirmation> goodsToValidate);
    }

    public class MockReception : IReception, IPrintable
    {
        private readonly DateTime _receptionDate;

        public MockReception(string id, params IReceptionGood[] goods)
            : this(id, DateTime.Now, goods)
        {
        }

        public MockReception(string id, DateTime receptionDate, params IReceptionGood[] goods)
            : this(id, receptionDate, new ListOfEntities<IReceptionGood>(goods))
        {
        }

        public MockReception(string id, DateTime receptionDate, IEntities<IReceptionGood> goods)
            : this(id, receptionDate, new MockReceptionGoods(goods))
        {
        }

        public MockReception(string id, DateTime receptionDate, IReceptionGoods goods)
        {
            Id = id;
            _receptionDate = receptionDate;
            Goods = goods;
        }

        public IReceptionGoods Goods { get; }

        public List<IGoodConfirmation> ValidatedGoods { get; } = new List<IGoodConfirmation>();

        public string Id { get; }

        public override bool Equals(object? obj)
        {
            return ReferenceEquals(this, obj)
                || (obj is MockReception reception && _receptionDate == reception._receptionDate)
                || (obj is DateTime receptionDate && receptionDate.Date == _receptionDate.Date);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, _receptionDate, Goods);
        }

        public void PrintTo(IMedia media)
        {
            media
                .Put("Id", Id)
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
