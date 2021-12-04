using System.Threading.Tasks;

namespace Warehouse.Core.Plugins
{
    public static class AsyncDataExtensions
    {
        public static async Task WaitWhileLoading<T>(this IAsyncData<T> data)
        {
            _ = data.Value;
            while (data.IsLoading)
            {
                await Task.Yield();
            }
        }
    }
}
