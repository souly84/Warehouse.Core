using System;

namespace Warehouse.Core.Plugins
{
    public static class ScannerExtensions
    {
        public static void CheckIfOpened(this IScanner scanner)
        {
            if (scanner == null || scanner.State != ScannerState.Opened)
            {
                throw new InvalidOperationException($"Scanner is not opened. Call {nameof(IScanner.OpenAsync)} first.");
            }
        }
    }
}
