using System.Threading.Tasks;
using PortaCapena.OdooJsonRpcClient;
using PortaCapena.OdooJsonRpcClient.Converters;
using PortaCapena.OdooJsonRpcClient.Models;

namespace Warehouse.Core.Tests.Extensions
{
    public static class OdooClientExtensions
    {
        public static Task<string> DotNetModel(this OdooClient client, IOdooModel forModel)
        {
            return client.DotNetModel(forModel.TableName());
        }

        public static async Task<string> DotNetModel(this OdooClient client, string tableName)
        {
            return OdooModelMapper.GetDotNetModel(
                tableName,
                (await client.GetModelAsync(tableName)).Value
            );
        }
    }
}
