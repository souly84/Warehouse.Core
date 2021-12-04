using System;
using System.Threading.Tasks;
using Xunit;
using Warehouse.Core.Plugins;

namespace Warehouse.Core.Tests
{
    public class PredicateExtensionsTests
    {
        [Fact]
        public void FuncTaskSyncRun()
        {
            var taskResult = false;
            Func<Task> func = async () =>
            {
                await Task.Delay(50);
                taskResult = true;
            };
            func.RunSync();
            Assert.True(taskResult);
        }

        [Fact]
        public void FuncTaskWithResultSyncRun()
        {
            Func<Task<bool>> func = async () =>
            {
                await Task.Delay(50);
                return true;
            };
            Assert.True(func.RunSync());
        }
    }
}
