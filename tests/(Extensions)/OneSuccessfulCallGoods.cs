using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Warehouse.Core.Tests.Extensions
{
    public class OneSuccessfulCallGoods : IGoods
    {
        private bool _wasCalled;
        public Task<IList<IGood>> ToListAsync()
        {
            if (_wasCalled)
            {
                throw new InvalidOperationException("The method has been called twice");
            }
            _wasCalled = true;
            return Task.FromResult<IList<IGood>>(new List<IGood>
            {
                new MockGood("1", 1),
                new MockGood("2", 1),
            });
        }

        public IGoods With(IFilter filter)
        {
            return new OneSuccessfulCallGoods();
        }
    }
}
