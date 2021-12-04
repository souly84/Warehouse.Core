namespace Warehouse.Core.Pugins
{
    public interface IPropertyChanged
    {
        void RaisePropertyChanged(string propertyName);
    }
}
