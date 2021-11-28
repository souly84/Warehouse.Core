using System.Threading.Tasks;

namespace Warehouse.Core
{
    public interface IConfirmationState
    {
        Task<ConfirmationState> ToEnumAsync();

        public enum ConfirmationState
        {
            NotStarted,
            Partially,
            Confirmed
        }
    }
}
