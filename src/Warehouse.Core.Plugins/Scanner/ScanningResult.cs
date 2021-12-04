using System;

namespace Warehouse.Core.Plugins
{
    public class ScanningResult : IScanningResult
    {
        public ScanningResult(string barcodeData, string symbology, TimeSpan timestamp)
        {
            BarcodeData = barcodeData;
            Symbology = symbology;
            Timestamp = timestamp;
        }

        public string BarcodeData { get; }

        public string Symbology { get; }

        public TimeSpan Timestamp { get; }

        public override bool Equals(object obj)
        {
            return obj is IScanningResult result
                && BarcodeData == result.BarcodeData
                && Symbology == result.Symbology
                && Timestamp == result.Timestamp;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(BarcodeData, Symbology, Timestamp);
        }
    }
}
