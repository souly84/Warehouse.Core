using PortaCapena.OdooJsonRpcClient;

namespace Warehouse.Core.Tests
{
    public class OdooWarehouse : IWarehouse
    {
        private readonly OdooClient _client;

        public OdooWarehouse(OdooClient client)
        {
            _client = client;
        }

        public IReceptions Receptions => new OdooReceptions(_client);
    }
}
