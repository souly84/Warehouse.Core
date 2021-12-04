namespace Warehouse.Core.Plugins
{
    public interface IPropertyChanged
    {
        void RaisePropertyChanged(string propertyName);
    }
}
