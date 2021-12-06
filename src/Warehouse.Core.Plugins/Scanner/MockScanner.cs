using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Warehouse.Core.Plugins
{
    public class MockScanner : INotifyPropertyChanged, IScanner
    {
        private ScannerState _state= ScannerState.Closed;

        public event PropertyChangedEventHandler? PropertyChanged;

        public event EventHandler<IScanningResult>? OnScan;

        public ScannerState State
        {
            get => _state;
            private set
            {
                _state = value;
                OnPropertyChanged();
            }
        }

        public void Scan(IScanningResult result)
        {
            OnScan?.Invoke(this, result);
        }

        public void BeepFailure()
        {
            // Nothing to do
        }

        public void BeepSuccess()
        {
            // Nothing to do
        }

        public Task OpenAsync()
        {
            return Task.CompletedTask;
        }

        public Task CloseAsync()
        {
            State = ScannerState.Closed;
            return Task.CompletedTask;
        }

        public Task EnableAsync(bool enabled)
        {
            State = ScannerState.Enabled;
            return Task.CompletedTask;
        }

        private void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
