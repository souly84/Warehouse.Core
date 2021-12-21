namespace Warehouse.Core
{
    public static class StoragesExtension
    {
        public static IStorages InLocalFirst(this IStorages storages)
        {
            return new InLocalFirstStorages(storages);
        }
    }
}
