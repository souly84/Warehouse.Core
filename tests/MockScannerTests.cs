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
        public void DoesNotRaisedEventWhenUnsubscribed()
        {
            IScanningResult expected = null;
            var scanner = new MockScanner();
            EventHandler<IScanningResult> handler = (sender, result) => expected = result;
            scanner.OnScan += handler;
            scanner.OnScan -= handler;
            scanner.Scan(
                new ScanningResult("TestBarcode", "TestSymbology", TimeSpan.FromSeconds(23))
            );
            Assert.Null(
                expected
            );
        }

        [Fact]
        public async Task NotifyOnEnableStateChanged()
        {
            var scanner = new MockScanner();
            await scanner.OpenAsync();
            await Assert.PropertyChangedAsync(
                scanner,
                nameof(MockScanner.State),
                () => scanner.EnableAsync(true)
            );
        }

        [Fact]
        public async Task Open()
        {
            var scanner = new MockScanner();
            await scanner.OpenAsync();
            Assert.Equal(
                ScannerState.Opened,
                scanner.State
            );
        }

        [Fact]
        public async Task Close()
        {
            var scanner = new MockScanner();
            await scanner.OpenAsync();
            await scanner.CloseAsync();
            Assert.Equal(
                ScannerState.Closed,
                scanner.State
            );
        }

        [Fact]
        public async Task Enabled()
        {
            var scanner = new MockScanner();
            await scanner.OpenAsync();
            await scanner.EnableAsync(true);
            Assert.Equal(
                ScannerState.Enabled,
                scanner.State
            );
        }

        [Fact]
        public async Task Disabled()
        {
            var scanner = new MockScanner();
            await scanner.OpenAsync();
            await scanner.EnableAsync(true);
            await scanner.EnableAsync(false);
            Assert.Equal(
                ScannerState.Opened,
                scanner.State
            );
        }

        [Fact]
        public void BeepSuccess()
        {
            var scanner = new MockScanner();
            scanner.BeepSuccess();
            Assert.Equal(
                1,
                scanner.BeepSuccessCount
            );
        }

        [Fact]
        public void BeepFailure()
        {
            var scanner = new MockScanner();
            scanner.BeepFailure();
            Assert.Equal(
                1,
                scanner.BeepFailureCount
            );
        }

        [Fact]
        public Task ThrowInvalidOperationException_OnEnableButNotOpened()
        {
            return Assert.ThrowsAsync<InvalidOperationException>(
                () => new MockScanner().EnableAsync(true)
            );
        }
    }
}
