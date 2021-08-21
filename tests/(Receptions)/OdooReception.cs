using System.Threading.Tasks;
using PortaCapena.OdooJsonRpcClient;
using Warehouse.Core.Tests.Goods;

namespace Warehouse.Core.Tests
{
    public class OdooReception : IReception
    {
        private readonly OdooClient _client;
        private readonly OdooStockPickingDto _odooReception;

        public OdooReception(
            OdooClient client,
            OdooStockPickingDto odooReception)
        {
            _client = client;
            _odooReception = odooReception;
        }

        public IGoods Goods => new OdooStockGoods(_client, _odooReception.Id);

        public Task ConfirmAsync(IGood good)
        {
            return Task.CompletedTask;
        }

        public Task ValidateAsync()
        {
            return Task.CompletedTask;
        }
    }
}
