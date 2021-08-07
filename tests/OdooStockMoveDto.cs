using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using PortaCapena.OdooJsonRpcClient.Attributes;
using PortaCapena.OdooJsonRpcClient.Converters;
using PortaCapena.OdooJsonRpcClient.Models;

namespace Warehouse.Core.Tests
{
    [OdooTableName("stock.move")]
    [JsonConverter(typeof(OdooModelConverter))]
    public class OdooStockMoveDto : IOdooModel
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        // required
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description_picking")]
        public string DescriptionPicking { get; set; }

        // required
        [JsonProperty("date")]
        public DateTime Date { get; set; }

        // required
        [JsonProperty("product_uom_qty")]
        public double ProductUomQty { get; set; }

        // required
        [JsonProperty("procure_method")]
        public SupplyMethodStockMoveOdooEnum ProcureMethod { get; set; }
    }

    // By default, the system will take from the stock in the source location and passively wait for availability. The other possibility allows you to directly create a procurement on the source location (and thus ignore its current stock) to gather products. If we want to chain moves and have this one to wait for the previous, this second option should be chosen.
    [JsonConverter(typeof(StringEnumConverter))]
    public enum SupplyMethodStockMoveOdooEnum
    {
        [EnumMember(Value = "make_to_stock")]
        DefaultTakeFromStock = 1,

        [EnumMember(Value = "make_to_order")]
        AdvancedApplyProcurementRules = 2,
    }
}

