using System;
using System.Threading;
using System.Threading.Tasks;

namespace Warehouse.Core.Pugins
{
    public static class PredicateExtensions
    {
        private static readonly TaskFactory TaskFactory = new TaskFactory(
            CancellationToken.None,
            TaskCreationOptions.None,
            TaskContinuationOptions.None,
            TaskScheduler.Default
        );

        public static TResult RunSync<TResult>(this Func<Task<TResult>> func)
        {
            return TaskFactory
              .StartNew(func)
              .Unwrap()
              .GetAwaiter()
              .GetResult();
        }

        public static void RunSync(this Func<Task> func)
        {
            TaskFactory
              .StartNew(func)
              .Unwrap()
              .GetAwaiter()
              .GetResult();
        }

        public static Task WaitForAsync(this Func<bool> predicate)
        {
            return predicate.WaitForAsync(60000);
        }

        public static Task WaitForAsync(
            this Func<bool> predicate,
            long timeoutInMilliseconds)
        {
            return predicate.WaitForAsync(timeoutInMilliseconds, 100);
        }

        public static async Task WaitForAsync(
            this Func<bool> predicate,
            long timeoutInMilliseconds,
            long operationInBetweenDelayInMilliseconds)
        {
            long operationTimeInMilliseconds = 0;
            while (!predicate())
            {
                await Task.Delay(TimeSpan.FromMilliseconds(operationInBetweenDelayInMilliseconds)).ConfigureAwait(false);
                operationTimeInMilliseconds += operationInBetweenDelayInMilliseconds;
                if (operationTimeInMilliseconds > timeoutInMilliseconds)
                {
                    throw new TaskCanceledException("Operation timeout has been exceeded");
                }
            }
        }
    }
}
