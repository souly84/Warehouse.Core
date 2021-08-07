using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PortaCapena.OdooJsonRpcClient;
using System.Linq;
using Newtonsoft.Json.Linq;
using PortaCapena.OdooJsonRpcClient.Models;

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
            var tableName = "stock.picking";
            var modelResult = await _client.GetModelAsync(tableName);

            //var model = OdooModelMapper.GetDotNetModel(tableName, modelResult.Value);

            var repository = new OdooRepository<OdooStockPickingDto>(_client.Config);
            var receptions = await repository.Query().ToListAsync();
            return receptions.Value.Select(reception => new OdooReception(_client, reception)).ToList<IReception>();
        }
    }

    public class OdooJObject : JObject, IOdooModel
    {
        public long Id { get => this["id"].Value<long>(); set { this["id"] = value; } }
    }
}
