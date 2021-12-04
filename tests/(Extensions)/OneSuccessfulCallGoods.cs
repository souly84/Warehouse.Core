using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Warehouse.Core.Tests.Extensions
{
    public class OnSuccesfulCallEntities<T> : IEntities<T>
    {
        private readonly T[] _entities;
        private bool _wasCalled;

        public OnSuccesfulCallEntities(params T[] entities)
        {
            _entities = entities;
        }

        public Task<IList<T>> ToListAsync()
        {
            if (_wasCalled)
            {
                throw new InvalidOperationException("The method has been called twice");
            }
            _wasCalled = true;
            return Task.FromResult<IList<T>>(new List<T>(_entities));
        }

        public IEntities<T> With(IFilter filter)
        {
            return new OnSuccesfulCallEntities<T>(_entities);
        }
    }
}
