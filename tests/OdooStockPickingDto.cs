using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using PortaCapena.OdooJsonRpcClient.Attributes;
using PortaCapena.OdooJsonRpcClient.Converters;
using PortaCapena.OdooJsonRpcClient.Models;

namespace Warehouse.Core.Tests
{
    [OdooTableName("stock.picking")]
    [JsonConverter(typeof(OdooModelConverter))]
    public class OdooStockPickingDto : IOdooModel
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("picking_type_code")]
        public TypeOfOperationStockPickingOdooEnum? PickingTypeCode { get; set; }

        [JsonProperty("move_ids_without_package")]
        public long[] MoveIdsWithoutPackage { get; set; }
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum TypeOfOperationStockPickingOdooEnum
    {
        [EnumMember(Value = "incoming")]
        Receipt = 1,

        [EnumMember(Value = "outgoing")]
        Delivery = 2,

        [EnumMember(Value = "internal")]
        InternalTransfer = 3,
    }
}
