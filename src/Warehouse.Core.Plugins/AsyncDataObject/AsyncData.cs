using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Warehouse.Core.Pugins
{
    public class AsyncData<T> : IAsyncData<T>
    {
        private readonly Func<Task<T?>> _asyncData;
        private readonly IPropertyChanged? _owner;
        private readonly T? _defaultValue;
        private readonly string? _name;
        private readonly object _syncObject = new object();
        private bool _forceDataLoad = true;
        private Task<T?>? _dataTask;

        public AsyncData(T? asyncData)
          : this(() => Task.FromResult(asyncData), default)
        {
        }

        public AsyncData(Task<T?> asyncData)
          : this(asyncData, default)
        {
        }

        public AsyncData(Task<T?> asyncData, T? defaultValue)
          : this(() => asyncData, defaultValue)
        {
        }

        public AsyncData(Func<Task<T?>> asyncData)
           : this(asyncData, default)
        {
        }

        public AsyncData(Func<Task<T?>> asyncData, T? defaultValue)
           : this(asyncData, null, defaultValue)
        {
        }

        public AsyncData(
            Func<Task<T?>> asyncData,
            IPropertyChanged owner,
            [CallerMemberName] string? name = null)
            : this(asyncData, owner, default, name)
        {
        }

        public AsyncData(
            Func<Task<T?>> asyncData,
            IPropertyChanged? owner,
            T? defaultValue,
            [CallerMemberName] string? name = null)
        {
            _asyncData = asyncData;
            _owner = owner;
            _defaultValue = defaultValue;
            _name = name;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private Task<T?> DataTask
        {
            get
            {
                if (_forceDataLoad)
                {
                    lock (_syncObject)
                    {
                        if (_forceDataLoad)
                        {
                            _dataTask = _asyncData() ?? throw new ArgumentNullException(nameof(DataTask));
                            _forceDataLoad = false;
                        }
                    }
                }
                return _dataTask ?? Task.FromResult(default(T));
            }
        }

        public bool IsLoading => !DataTask.IsCompleted;

        public T? Value
        {
            get
            {
                var dataTask = DataTask;
                if (dataTask.IsCompleted)
                {
                    if (dataTask.IsFaulted)
                    {
                        throw dataTask.Exception.Unwrap();
                    }
                    return dataTask.Result ?? default;
                }

                NotifyPropertyChanged(nameof(IsLoading));
                _ = dataTask.ContinueWith(OnDataLoadingComplete, TaskScheduler.Current);
                return _defaultValue ?? default;
            }
        }

        public void Refresh()
        {
            _forceDataLoad = true;
            NotifyPropertyChanged(nameof(Value));
        }

        private Task OnDataLoadingComplete(Task task)
        {
            if (!string.IsNullOrEmpty(_name))
            {
                _owner?.RaisePropertyChanged(_name);
            }
           
            NotifyPropertyChanged(nameof(IsLoading));
            NotifyPropertyChanged(nameof(Value));
            return task;
        }

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
