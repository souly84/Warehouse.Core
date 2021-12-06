using System;
using System.Threading.Tasks;

namespace Warehouse.Core.Plugins
{
    public interface IScanner
    {
        event EventHandler<IScanningResult> OnScan;

        ScannerState State { get; }

        Task OpenAsync();

        Task CloseAsync();

        Task EnableAsync(bool enabled);

        void BeepSuccess();

        void BeepFailure();
    }
}
