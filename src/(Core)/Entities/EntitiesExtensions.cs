using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Warehouse.Core
{
    public static class EntitiesExtensions
    {
        public static IEntities<T> Cached<T>(this IEntities<T> entities)
        {
            return new CachedEntities<T>(entities);
        }

        public static async Task<IEnumerable<T>> WhereAsync<T>(
            this IEntities<T> entities,
            Func<T, bool> predicate)
        {
            var goodsList = await entities.ToListAsync();
            return goodsList.Where(predicate);
        }
    }
}
