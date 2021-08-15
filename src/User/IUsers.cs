using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Warehouse.Core.Core;

namespace Warehouse.Core
{
    public interface IUsers
    {
        Task<IList<IUser>> ToListAsync();
        IUsers With(IFilter filter);
    }
}
