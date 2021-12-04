using System;
namespace Warehouse.Core.Pugins
{
    public static class ExceptionExtensions
    {
        public static Exception Unwrap(this Exception exception)
        {
            Exception ex = exception;
            while (ex is AggregateException && ex.InnerException != null)
            {
                ex = ex.InnerException;
            }
            return ex;
        }
    }
}
