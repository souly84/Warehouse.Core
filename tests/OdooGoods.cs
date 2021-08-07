using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PortaCapena.OdooJsonRpcClient;
using PortaCapena.OdooJsonRpcClient.Request;


namespace Warehouse.Core.Tests
{
    public class OdooGoods : IGoods
    {
        private readonly OdooClient _client;

        public OdooGoods(OdooClient client)
        {
            _client = client;
        }

        public async Task<IList<IGood>> ToListAsync()
        {
            var repository = new OdooRepository<OdooStockMoveDto>(_client.Config);
            var goods = await repository.Query()
                .ToListAsync();
            return goods.Value.Select(good => new OdooGood(_client, good)).ToList<IGood>();
        }
    }
}
