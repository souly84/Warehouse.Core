using System.Threading.Tasks;
using Xunit;

namespace Warehouse.Core.Tests
{
    public class TaskExtensionsTests
    {
        [Fact]
        public void TaskSyncRun()
        {
            var taskResult = false;
            Task.Run(() => taskResult = true).RunSync();
            Assert.True(taskResult);
        }

        [Fact]
        public void TaskWithResultSyncRun()
        {
            Assert.True(Task.Run(() => true).RunSync());
        }

        [Fact]
        public void TaskFuncSyncRun()
        {
            var taskResult = false;
            Task.Run(async () =>
            {
                await Task.Delay(100);
                taskResult = true;
            }).RunSync();
            Assert.True(taskResult);
        }
        [Fact]
        public void TaskFuncWithResultSyncRun()
        {
            Assert.True(Task.Run(async  () =>
            {
                await Task.Delay(100);
                return true;
            }).RunSync());
        }
    }
}
