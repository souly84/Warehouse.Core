using System;
using System.Threading.Tasks;
using Warehouse.Core.Plugins;
using Xunit;

namespace Warehouse.Core.Tests
{
    public class MockScannerTests
    {
        [Fact]
        public void RaisesEventWhenScanned()
        {
            IScanningResult expected = null;
            var scanner = new MockScanner();
            scanner.OnScan += (sender, result) => expected = result;
            scanner.Scan(
                new ScanningResult("TestBarcode", "TestSymbology", TimeSpan.FromSeconds(23))
            );
            Assert.Equal(
                new ScanningResult("TestBarcode", "TestSymbology", TimeSpan.FromSeconds(23)),
                expected
            );
        }

        [Fact]
        public Task NotifyOnEnableStateChanged()
        {
            var scanner = new MockScanner();
            return Assert.PropertyChangedAsync(
                scanner,
                nameof(MockScanner.State),
                () => scanner.EnableAsync(true)
            );
        }
    }
}
