using System;
using System.Threading.Tasks;
using Xunit;
using Warehouse.Core.Plugins;

namespace Warehouse.Core.Tests
{
    public class TaskExtensionsTests
    {
        [Fact]
        public void TaskSyncRun()
        {
            var taskResult = false;
            Task.Run(() =>
            {
                taskResult = true;
            }).RunSync();
            Assert.True(taskResult);
        }

        [Fact]
        public void TaskWithResultSyncRun()
        {
            Assert.True(Task.Run(() => true).RunSync());
        }

        [Fact]
        public void FireAndForget()
        {
            var taskResult = false;
            Task.Run(() =>
            {
                taskResult = true;
            }).FireAndForget();
            Assert.True(taskResult);
        }

        [Fact]
        public async Task FireAndForgetWithException()
        {
            Exception expectedException = null;
            Task.Run(() =>
            {
                throw new InvalidOperationException("Test Exception");
            }).FireAndForget((ex) => expectedException = ex);
            Func<bool> waitingForResult = () => expectedException != null;
            await waitingForResult.WaitForAsync();
            Assert.NotNull(expectedException);
            Assert.IsType<InvalidOperationException>(expectedException);
        }

        [Fact]
        public async Task ThenWithAction()
        {
            int count = 0;
            await Task
                .Run(() => count++)
                .Then(() => count++);
            Assert.Equal(2, count);
        }


        [Fact]
        public async Task ThenWithTask()
        {
            int count = 0;
            await Task
                .Run(() => count++)
                .Then(async () =>
                {
                    await Task.Delay(50).ConfigureAwait(false);
                    count++;
                });
            Func<bool> waitingForResult = () => count == 2;
            await waitingForResult.WaitForAsync();
            Assert.Equal(2, count);
        }

        [Fact]
        public async Task ThenNotCalledWhenException()
        {
            int count = 0;
            await Assert.ThrowsAsync<InvalidCastException>(() =>
                Task.Run(() =>
                {
                    count++;
                    throw new InvalidCastException("Test Exception");
                })
                .Then(() => count++)
            );
            Assert.Equal(1, count);
        }

        [Fact]
        public Task TaskTimeout_DoesNotThrowUnhandledException_EvenWhenAnExceptionOccurs()
        {
            return Assert.ThrowsAsync<InvalidOperationException>(() =>
                Assert.DoesNotThrowUnobservedTaskException(() => Task.Run<object>(async () =>
                {
                    await Task.Delay(100).ConfigureAwait(false);
                    throw new InvalidOperationException("Test exception");
                }).WithTimeout(1000)
            ));
        }

        [Fact]
        public Task TaskTimeout_ThrowsTaskCancelledException_WhenTimeoutExceeded()
        {
            return Assert.ThrowsAsync<TaskCanceledException>(() =>
                Assert.DoesNotThrowUnobservedTaskException(() => Task.Run<object>(async () =>
                {
                    await Task.Delay(2000).ConfigureAwait(false);
                    throw new InvalidOperationException("Test exception");
                }).WithTimeout(1000)
            ));
        }
    }
}
