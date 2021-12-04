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
        public async Task NotifyOnEnableStateChanged()
        {
            string expected = null;
            var scanner = new MockScanner();
            scanner.PropertyChanged += (sender, args) => expected = args.PropertyName;
            await scanner.EnableAsync(true);
            Assert.Equal(
                nameof(MockScanner.IsEnabled),
                expected
            );
        }
    }
}
