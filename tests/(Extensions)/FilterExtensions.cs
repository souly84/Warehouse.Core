using MediaPrint;

namespace Warehouse.Core.Tests.Extensions
{
    public static class FilterExtensions
    {
        public static DictionaryMedia ToDictionary(this IFilter filter)
        {
            var dictionary = new DictionaryMedia();
            var filterDictionary = filter.ToParams();
            foreach (var key in filterDictionary.Keys)
            {
                dictionary.Put(key, filterDictionary[key]);
            }

            return dictionary;
        }
    }
}
