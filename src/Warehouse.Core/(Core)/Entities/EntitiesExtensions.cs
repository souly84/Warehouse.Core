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

        public static async Task<T> FirstAsync<T>(
            this IEntities<T> entities)
        {
            var entitiesList = await entities.ToListAsync();
            return entitiesList.First();
        }

        public static async Task<T> FirstAsync<T>(
            this IEntities<T> entities,
            Func<T, bool> predicate)
        {
            var entitiesList = await entities.ToListAsync();
            return entitiesList.First(predicate);
        }

        public static async Task<T> FirstAsync<T>(
            this IEntities<T> entities,
            Func<T, Task<bool>> predicateAsync)
        {
            var entitiesList = await entities.ToListAsync();
            return await entitiesList.FirstAsync(predicateAsync);
        }

        public static async Task<T> FirstOrDefaultAsync<T>(
            this IEntities<T> entities,
            Func<T, bool> predicate)
        {
            var entitiesList = await entities.ToListAsync();
            return entitiesList.FirstOrDefault(predicate);
        }

        public static async Task<IEnumerable<T>> WhereAsync<T>(
            this IEntities<T> entities,
            Func<T, bool> predicate)
        {
            var entitiesList = await entities.ToListAsync();
            return entitiesList.Where(predicate);
        }

        public static async Task<IEnumerable<TResult>> SelectAsync<T, TResult>(
            this IEntities<T> entities,
            Func<T, TResult> entityMap)
        {
            var entitiesList = await entities.ToListAsync();
            return entitiesList.Select(entityMap);
        }
    }
}
