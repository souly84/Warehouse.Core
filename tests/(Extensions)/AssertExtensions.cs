using System;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Xunit.Abstractions;

namespace Warehouse.Core.Tests
{
    public class Assert : Xunit.Assert
    {
        public static async Task EqualAsync<T>(IEntities<T> expected, IEntities<T> actual)
        {
            var expectedList = await expected.ToListAsync();
            var actualList = await actual.ToListAsync();

            Xunit.Assert.Equal(expectedList, actualList);
        }

        public static void EqualJson(string expectedJson, string actualJson, ITestOutputHelper output = null)
        {
            JObject expected = JObject.Parse(expectedJson);
            JObject actual = JObject.Parse(actualJson);
            if (output != null)
            {
                output.WriteLine("Expected:" + expectedJson);
                output.WriteLine("Actual:" + actualJson);
            }

            Xunit.Assert.Equal(expected, actual, JToken.EqualityComparer);
        }

        public static async Task DoesNotThrowUnobservedTaskException(Func<Task> method)
        {
            Exception unobservedTaskException = null;
            EventHandler<UnobservedTaskExceptionEventArgs> proc = (sender, args) =>
            {
                unobservedTaskException = args.Exception;
                args.SetObserved();
            };

            try
            {
                TaskScheduler.UnobservedTaskException += proc;
                await method().ConfigureAwait(false);
            }
            finally
            {
                await WaitForGCCollectAsync().ConfigureAwait(false);
                TaskScheduler.UnobservedTaskException -= proc;
            }

            Xunit.Assert.Null(unobservedTaskException);
        }

        private static async Task WaitForGCCollectAsync()
        {
            // we do it several times not to miss TaskScheduler.UnobservedTaskException call if unhandled exception occured
            for (var i = 0; i < 5; i++)
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                await Task.Delay(1000).ConfigureAwait(false);
            }
        }
    }
}
