using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Warehouse.Core.Plugins
{
    public class MockScanner : INotifyPropertyChanged, IScanner
    {
        private bool _isEnabled;

        public event PropertyChangedEventHandler? PropertyChanged;

        public event EventHandler<IScanningResult>? OnScan;

        public bool IsEnabled
        {
            get => _isEnabled;
            private set
            {
                _isEnabled = value;
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
            IsEnabled = false;
            return Task.CompletedTask;
        }

        public Task EnableAsync(bool enabled)
        {
            IsEnabled = enabled;
            return Task.CompletedTask;
        }

        private void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
