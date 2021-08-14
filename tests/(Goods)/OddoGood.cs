using PortaCapena.OdooJsonRpcClient;

namespace Warehouse.Core.Tests.Goods
{
    public class OddoGood : IGood
    {
        private readonly OdooClient _client;
        private readonly OdooStockGoodDto _odooGood;

        public OddoGood(
            OdooClient client,
            OdooStockGoodDto odooGood)
        {
            _client = client;
            _odooGood = odooGood;
        }
    }
}
