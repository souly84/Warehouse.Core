using Newtonsoft.Json;
using PortaCapena.OdooJsonRpcClient.Attributes;
using PortaCapena.OdooJsonRpcClient.Converters;
using PortaCapena.OdooJsonRpcClient.Models;

namespace Warehouse.Core.Tests
{
    [OdooTableName("stock.move")]
    [JsonConverter(typeof(OdooModelConverter))]
    public class OdooStockGoodDto : IOdooModel
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        // required
        [JsonProperty("name")]
        public string Name { get; set; }

        // product.product
        // required
        [JsonProperty("product_id")]
        public long ProductId { get; set; }

        [JsonProperty("description_picking")]
        public string DescriptionPicking { get; set; }

        [JsonProperty("product_qty")]
        public double? ProductQty { get; set; }
    }
}
