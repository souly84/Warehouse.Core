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
