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
        public async Task FireAndForget()
        {
            var taskResult = false;
            Task.Run(() =>
            {
                taskResult = true;
            }).FireAndForget();
            Func<bool> waitingForResult = () => taskResult == true;
            await waitingForResult.WaitForAsync();
            Assert.True(taskResult);
        }

        [Fact]
        public async Task FireAndForgetWithException()
        {
            Exception expectedException = null;
            Task
                .Run(() => throw new InvalidOperationException("Test Exception"))
                .FireAndForget((ex) => expectedException = ex);
            Func<bool> waitingForResult = () => expectedException != null;
            await waitingForResult.WaitForAsync();
            Assert.NotNull(expectedException);
            Assert.IsType<InvalidOperationException>(expectedException);
        }

        [Fact]
        // The main goal for this test is to check that FireAndForget catches the exception
        public void FireAndForgetWithExceptionButNoAction()
        {
            Task
                .Run(() => throw new InvalidOperationException("Test Exception"))
                .FireAndForget();
            Assert.True(true);
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
        public async Task ThenWithTaskArgumentAction()
        {
            int count = 0;
            await Task
                .Run(() => count++)
                .Then((Task taskResult) =>
                {
                    count++;
                });
            Assert.Equal(2, count);
        }

        [Fact]
        public async Task ThenWithTaskArgumentAndFunc()
        {
            int count = 0;
            await Task
                .Run(() => count++)
                .Then((Task taskResult) =>
                {
                    count++;
                    return Task.CompletedTask;
                });
            Assert.Equal(2, count);
        }

        [Fact]
        public async Task ThenWithFuncTask()
        {
            int count = 0;
            await Task
                .Run(() => count++)
                .Then(() =>
                {
                    count++;
                    return Task.CompletedTask;
                });
            Assert.Equal(2, count);
        }

        [Fact]
        public async Task ThenWithTaskGenericResultArgumentAndFunc()
        {
            int count = 0;
            await Task
                .Run(() =>
                {
                    return count + 1;
                })
                .Then((int value) =>
                {
                    count += value;
                    return Task.CompletedTask;
                });
            Assert.Equal(1, count);
        }

        [Fact]
        public async Task ThenWithTaskResultArgumentAction()
        {
            int count = 0;
            await Task
                .Run(() =>
                {
                    count += 2;
                    return count;
                })
                .Then((int value) =>
                {
                    count += value;
                });
            Assert.Equal(4, count);
        }

        [Fact]
        public async Task ThenWithTaskResultArgumentActionAndFunc()
        {
            int count = 0;
            Assert.Equal(
                4,
                await Task.Run(() =>
                {
                    count += 2;
                    return count;
                }).Then(async (int value) =>
                {
                    count += value;
                    await Task.Delay(50).ConfigureAwait(false);
                    return count;
                })
            );
        }

        [Fact]
        public Task ThenWithNullActionThrowsArgumentNullException()
        {
            return Assert.ThrowsAsync<ArgumentNullException>(() =>
                Task
                    .Run(() => Task.CompletedTask)
                    .Then((Action)null)
            );
        }

        [Fact]
        public Task ThenWithNullTaskActionThrowsArgumentNullException()
        {
            return Assert.ThrowsAsync<ArgumentNullException>(() =>
                Task
                    .Run(() => Task.CompletedTask)
                    .Then((Action<Task>)null)
            );
        }

        [Fact]
        public Task ThenWithNullThrowsArgumentNullException()
        {
            int count = 0;
            return Assert.ThrowsAsync<ArgumentNullException>(() =>
                ((Task)null).Then(() => count++)
            );
        }

        [Fact]
        public Task ThenWithNullGenericThrowsArgumentNullException()
        {
            int count = 0;
            return Assert.ThrowsAsync<ArgumentNullException>(() =>
                ((Task<int>)null).Then(() => count++)
            );
        }

        [Fact]
        public Task ThenWithNullAndActionTaskThrowsArgumentNullException()
        {
            int count = 0;
            return Assert.ThrowsAsync<ArgumentNullException>(() =>
                ((Task)null).Then(async () =>
                {
                    await Task.Delay(50).ConfigureAwait(false);
                    count++;
                })
            );
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
        public async Task TaskTimeout_DoesNotThrowTimeoutException_WhenNoTimeoutExceeded()
        {
            Assert.Equal(
                100,
                await Task.Run(async () =>
                {
                    await Task.Delay(100).ConfigureAwait(false);
                    return 100;
                }).WithTimeout(1000)
            );
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
