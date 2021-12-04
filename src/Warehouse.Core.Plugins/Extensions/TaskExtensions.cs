namespace System.Threading.Tasks
{
    public static class TaskExtensions
    {
        public static readonly TaskFactory TaskFactory = new TaskFactory(
            CancellationToken.None,
            TaskCreationOptions.None,
            TaskContinuationOptions.None,
            TaskScheduler.Default
        );

        public static TResult RunSync<TResult>(this Task<TResult> task)
        {
            return TaskFactory
              .StartNew(() => task)
              .Unwrap()
              .GetAwaiter()
              .GetResult();
        }

        public static void RunSync(this Task task)
        {
            TaskFactory
              .StartNew(() => task)
              .Unwrap()
              .GetAwaiter()
              .GetResult();
        }

        /// <summary>
        /// Fires the <see cref="Task" /> and safely forget.
        /// </summary>
        /// <param name="task">The task.</param>
        public static void FireAndForget(this Task task)
        {
            task.FireAndForget(null);
        }

        /// <summary>
        /// Fires the <see cref="Task"/> and safely forget and in case of exception call <paramref name="onError"/> handler if any.
        /// </summary>
        /// <param name="task">The task.</param>
        /// <param name="onError">The on error action handler.</param>
        public static async void FireAndForget(this Task task, Action<Exception>? onError)
        {
            try
            {
                await task.ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                onError?.Invoke(ex);
            }
        }

        public static async Task Then(this Task task, Action<Task> next)
        {
            _ = task ?? throw new ArgumentNullException(nameof(task));
            _ = next ?? throw new ArgumentNullException(nameof(next));
            await task.ConfigureAwait(false);
            next(task);
        }

        public static async Task Then(this Task task, Action next)
        {
            _ = task ?? throw new ArgumentNullException(nameof(task));
            _ = next ?? throw new ArgumentNullException(nameof(next));
            await task.ConfigureAwait(false);
            next();
        }

        public static async Task Then<T>(this Task<T> task, Action<T, Task> next)
        {
            _ = task ?? throw new ArgumentNullException(nameof(task));
            _ = next ?? throw new ArgumentNullException(nameof(next));
            var result = await task.ConfigureAwait(false);
            next(result, task);
        }

        public static async Task Then<T>(this Task<T> task, Action<T> next)
        {
            _ = task ?? throw new ArgumentNullException(nameof(task));
            _ = next ?? throw new ArgumentNullException(nameof(next));
            var result = await task.ConfigureAwait(false);
            next(result);
        }

        public static async Task Then(this Task task, Func<Task, Task> next)
        {
            _ = task ?? throw new ArgumentNullException(nameof(task));
            _ = next ?? throw new ArgumentNullException(nameof(next));
            await task.ConfigureAwait(false);
            await next(task).ConfigureAwait(false);
        }

        public static async Task Then(this Task task, Func<Task> next)
        {
            _ = task ?? throw new ArgumentNullException(nameof(task));
            _ = next ?? throw new ArgumentNullException(nameof(next));
            await task.ConfigureAwait(false);
            await next().ConfigureAwait(false);
        }

        public static async Task<T> Then<T>(this Task task, Func<Task<T>> next)
        {
            _ = task ?? throw new ArgumentNullException(nameof(task));
            _ = next ?? throw new ArgumentNullException(nameof(next));
            await task.ConfigureAwait(false);
            return await next().ConfigureAwait(false);
        }

        public static async Task Then<T>(this Task<T> task, Func<T, Task, Task> next)
        {
            _ = task ?? throw new ArgumentNullException(nameof(task));
            _ = next ?? throw new ArgumentNullException(nameof(next));
            var result = await task.ConfigureAwait(false);
            await next(result, task).ConfigureAwait(false);
        }

        public static async Task<T2> Then<T1, T2>(this Task<T1> task, Func<T1, Task<T2>> next)
        {
            _ = task ?? throw new ArgumentNullException(nameof(task));
            _ = next ?? throw new ArgumentNullException(nameof(next));
            var result = await task.ConfigureAwait(false);
            return await next(result).ConfigureAwait(false);
        }

        public static async Task Then<T>(this Task<T> task, Func<T, Task> next)
        {
            _ = task ?? throw new ArgumentNullException(nameof(task));
            _ = next ?? throw new ArgumentNullException(nameof(next));
            var result = await task.ConfigureAwait(false);
            await next(result).ConfigureAwait(false);
        }

        public static Task<T> WithTimeout<T>(this Task<T> task, int milliseconds)
        {
            return task.WithTimeout(
                TimeSpan.FromMilliseconds(milliseconds)
            );
        }

        public static async Task<T> WithTimeout<T>(this Task<T> task, TimeSpan timeout)
        {
            _ = task ?? throw new ArgumentNullException(nameof(task));
            var tcs = new TaskCompletionSource<T>();
            using (var cts = new CancellationTokenSource())
            {
                using (var registration = cts.Token.Register(() => tcs.TrySetCanceled()))
                {
                    cts.CancelAfter(timeout);
                    _ = task
                        .ContinueWith(
                            t =>
                            {
                                if (t.Exception != null)
                                {
                                    tcs.TrySetException(t.Exception.InnerException);
                                }
                                else if (t.IsCanceled)
                                {
                                    tcs.TrySetCanceled();
                                }
                                else
                                {
                                    tcs.TrySetResult(t.Result);
                                }
                            },
                            TaskScheduler.Current
                        );

                    return await tcs.Task.ConfigureAwait(false);
                }
            }
        }
    }
}
