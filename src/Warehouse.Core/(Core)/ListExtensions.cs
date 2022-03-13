using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Warehouse.Core
{
    public static class ListExtensions
    {
        public static async Task<IList<T>> WhereAsync<T>(
            this IEnumerable<T> list,
            Func<T, Task<bool>> predicateAsync)
        {
            var result = new List<T>();
            foreach (var item in list)
            {
                if (await predicateAsync(item))
                {
                    result.Add(item);
                }
            }
            return result;
        }

        public static async Task<IList<T>> WhereAsync<T>(
            this Task<IEnumerable<T>> listTask,
            Func<T, Task<bool>> predicateAsync)
        {
            var result = new List<T>();
            foreach (var item in await listTask)
            {
                if (await predicateAsync(item))
                {
                    result.Add(item);
                }
            }
            return result;
        }

        public static async Task<IList<T>> WhereAsync<T>(
            this Task<IList<T>> listTask,
            Func<T, Task<bool>> predicateAsync)
        {
            var result = new List<T>();
            foreach (var item in await listTask)
            {
                if (await predicateAsync(item))
                {
                    result.Add(item);
                }
            }
            return result;
        }

        public static async Task<T> FirstAsync<T>(
            this IEnumerable<T> list,
            Func<T, Task<bool>> predicateAsync)
        {
            foreach (var item in list)
            {
                if (await predicateAsync(item))
                {
                    return item;
                }
            }
            throw new InvalidOperationException("No items found");
        }

        public static async Task<T> FirstAsync<T>(
            this Task<IEnumerable<T>> listTask)
        {
            return (await listTask).First();
        }

        public static async Task<T> FirstAsync<T>(
            this Task<IList<T>> listTask)
        {
            return (await listTask).First();
        }

        public static async Task<bool> AllAsync<T>(
            this IEnumerable<T> list,
            Func<T, Task<bool>> predicateAsync)
        {
            foreach (var item in list)
            {
                if (!await predicateAsync(item))
                {
                    return false;
                }
            }
            return true;
        }

        public static bool AtLeastOne<T>(
            this IList<T> list,
            IList<T> listWithItems)
        {
            return listWithItems.Any(
                item => list.Contains(item)
            );
        }

        public static async Task<T> FirstOrDefaultAsync<T>(
            this Task<IEnumerable<T>> listTask)
        {
            return (await listTask).FirstOrDefault();
        }

        public static async Task<T> FirstOrDefaultAsync<T>(
            this IEnumerable<T> list,
            Func<T, Task<bool>> predicateAsync)
        {
            foreach (var item in list)
            {
                if (await predicateAsync(item))
                {
                    return item;
                }
            }
            return default(T);
        }
    }
}
