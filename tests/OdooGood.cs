using System;
using PortaCapena.OdooJsonRpcClient;

namespace Warehouse.Core.Tests
{
    public class OdooGood : IGood
    {
        private readonly OdooClient _client;
        private readonly OdooStockMoveDto _odooStockMove;

        public OdooGood(OdooClient client, OdooStockMoveDto odooStockMove)
        {
            _client = client;
            _odooStockMove = odooStockMove;
        }
    }
}
