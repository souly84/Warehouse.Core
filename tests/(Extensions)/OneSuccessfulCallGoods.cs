using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Warehouse.Core.Receptions.Goods;

namespace Warehouse.Core.Tests.Extensions
{
    public class OnSuccesfulCallEntities : IEntities<IReceptionGood>
    {
        private bool _wasCalled;
        public Task<IList<IReceptionGood>> ToListAsync()
        {
            if (_wasCalled)
            {
                throw new InvalidOperationException("The method has been called twice");
            }
            _wasCalled = true;
            return Task.FromResult<IList<IReceptionGood>>(new List<IReceptionGood>
            {
                new MockReceptionGood("1", 1),
                new MockReceptionGood("2", 1),
            });
        }

        public IEntities<IReceptionGood> With(IFilter filter)
        {
            return new OnSuccesfulCallEntities();
        }
    }
}
