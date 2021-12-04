using System;
using System.Threading.Tasks;
using Warehouse.Core.Plugins;
using Xunit;

namespace Warehouse.Core.Tests
{
    public class AsyncDataTests
    {
        [Fact]
        public void IsLoadingTrueWhenAsyncTaskInProgress()
        {
            Assert.True(
                new AsyncData<bool>(
                    InfiniteLoadingTask<bool>()
                ).IsLoading
            );
        }

        [Fact]
        public void IsLoadingFalseWhenAsyncTaskComplete()
        {
            Assert.False(
                new AsyncData<bool>(
                    true
                ).IsLoading
            );
        }

        [Fact]
        public void ValueThrowsException_WhenLoadingTaskThrowsOne_SeveralRuns()
        {
            for (int i = 0; i < 10000; i++)
            {
                ValueThrowsException_WhenLoadingTaskThrowsOne();
            }
        }

        [Fact]
        public async Task IsLoadingFalseWhenDataLoaded()
        {
            var dataTask = new TaskCompletionSource<bool>();
            var asyncData = new AsyncData<bool>(dataTask.Task);
            var wait = asyncData.WaitWhileLoading();
            dataTask.SetResult(true);
            await wait.ConfigureAwait(false);
            Assert.False(asyncData.IsLoading);
            Assert.True(asyncData.Value);
        }

        [Fact]
        public void ReturnsDefaultValueWhileDataLoading()
        {
            Assert.Equal(
                10,
                new AsyncData<float>(
                    InfiniteLoadingTask<float>(),
                    defaultValue: 10f
                ).Value
            );
        }

        [Fact]
        public void ValueThrowsException_WhenLoadingTaskThrowsOne()
        {
            var dataTask = new TaskCompletionSource<bool>();
            var asyncData = new AsyncData<bool>(dataTask.Task);
            _ = asyncData.Value;
            Assert.True(asyncData.IsLoading);
            dataTask.SetException(new NotImplementedException());
            Assert.Throws<NotImplementedException>(() => asyncData.Value);
            Assert.False(asyncData.IsLoading);
        }

        [Fact]
        public void RaisesOnPropertyChanged_ForIsLoading_WhenStartLoadingData()
        {
            var asyncData = new AsyncData<bool>(InfiniteLoadingTask<bool>());
            Assert.PropertyChanged(
                asyncData,
                nameof(AsyncData<bool>.IsLoading),
                () => { _ = asyncData.Value; }
            );
        }

        [Fact]
        public Task RaisesOnPropertyChanged_ForIsLoading_WhenDataLoaded()
        {
            var loadingTask = new TaskCompletionSource<bool>();
            var asyncData = new AsyncData<bool>(loadingTask.Task);
            _ = asyncData.Value;
            return Assert.PropertyChangedAsync(
                asyncData,
                nameof(AsyncData<bool>.IsLoading),
                async () =>
                {
                    loadingTask.SetResult(true);
                    await Task.Delay(500).ConfigureAwait(false); // this one is needed to let the loading task completion to finish
                }
            );
        }

        [Fact]
        public Task RaisesOnPropertyChanged_ForValue_WhenDataLoaded()
        {
            var loadingTask = new TaskCompletionSource<bool>();
            var asyncData = new AsyncData<bool>(loadingTask.Task);
            _ = asyncData.Value;
            return Assert.PropertyChangedAsync(
                asyncData,
                nameof(AsyncData<bool>.Value),
                async () =>
                {
                    loadingTask.SetResult(true);
                    await Task.Delay(500).ConfigureAwait(false); // this one is needed to let the loading task completion to finish
                }
            );
        }

        private Task<T> InfiniteLoadingTask<T>()
        {
            return new TaskCompletionSource<T>().Task;
        }
    }
}
