using System.ComponentModel;

namespace Warehouse.Core.Pugins
{
    public interface IAsyncData<out T> : INotifyPropertyChanged
    {
        bool IsLoading { get; }

        T? Value { get; }

        void Refresh();
    }
}
