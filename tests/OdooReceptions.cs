using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PortaCapena.OdooJsonRpcClient;
using System.Linq;
using Newtonsoft.Json.Linq;
using PortaCapena.OdooJsonRpcClient.Models;
using PortaCapena.OdooJsonRpcClient.Request;

namespace Warehouse.Core.Tests
{
    public class OdooReceptions : IReceptions
    {
        private readonly OdooClient _client;

        public OdooReceptions(OdooClient client)
        {
            _client = client;
        }

        public async Task<IList<IReception>> ToListAsync()
        {
            var repository = new OdooRepository<OdooStockPickingDto>(_client.Config);
            var receptions = await repository.Query()
                .Where(OdooFilter.Create().EqualTo("picking_type_code", "incoming"))
                .ToListAsync();
            return receptions.Value.Select(reception => new OdooReception(_client, reception)).ToList<IReception>();
        }
    }

    public class OdooJObject : JObject, IOdooModel
    {
        public long Id { get => this["id"].Value<long>(); set { this["id"] = value; } }
    }
}
