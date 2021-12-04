using System;

namespace Warehouse.Core.Plugins
{
    public interface IScanningResult
    {
        string BarcodeData { get; }

        string Symbology { get; }

        TimeSpan Timestamp { get; }
    }
}
