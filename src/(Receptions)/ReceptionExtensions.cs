namespace Warehouse.Core.Receptions
{
    public static class ReceptionExtensions
    {
        public static IConfirmation Confirmation(this IReception reception)
        {
            return new ReceptionConfirmation(reception);
        }
    }
}
