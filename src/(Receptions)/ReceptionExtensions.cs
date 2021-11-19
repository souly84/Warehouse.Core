namespace Warehouse.Core.Receptions
{
    public static class ReceptionExtensions
    {
        public static IValidation Validation(this IReception reception)
        {
            return new ReceptionValidation(reception);
        }
    }
}
