using System;
using Newtonsoft.Json.Linq;
using PortaCapena.OdooJsonRpcClient;

namespace Warehouse.Core.Tests
{
    public class OdooReception : IReception
    {
        private readonly OdooClient _client;
        private readonly OdooStockPickingDto _odooReception;


        public OdooReception(OdooClient client, OdooStockPickingDto odooReception)
        {
            _client = client;
            _odooReception = odooReception;
        }

        public IGoods Goods => new OdooGoods(_client);
    }
}
