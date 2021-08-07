using System;
using System.Threading.Tasks;
using PortaCapena.OdooJsonRpcClient;
using PortaCapena.OdooJsonRpcClient.Converters;
using PortaCapena.OdooJsonRpcClient.Models;
using Xunit;
using System.Linq;

namespace Warehouse.Core.Tests
{
    public class Tests
    {
        private OdooCompany _odooCompany = new OdooCompany("https://testreception.odoo.com", "testreception");
        private async void ConnectToOdoo()
        {
            var config = new OdooConfig(
               apiUrl: "https://testreception.odoo.com", //  "http://localhost:8069"
               dbName: "testreception",
               userName: "zhukovskydenis@gmail.com",
               password: "mowmav-vande9-cUsfav"
               );

            var odooClient = new OdooClient(config);
            var versionResult = await odooClient.GetVersionAsync();

            var loginResult = await odooClient.LoginAsync();

            var tableName = "res.users";
            var modelResult = await odooClient.GetModelAsync(tableName);

            var userModel = OdooModelMapper.GetDotNetModel(tableName, modelResult.Value);

            //var repository = new OdooRepository<StockPickingOdooModel>(config);
            //var products = await repository.Query().ToListAsync();
        }

        [Fact]
        public async Task SuccesfullLogin()
        {
            var user = await _odooCompany.LoginAsync("zhukovskydenis@gmail.com", "mowmav-vande9-cUsfav");
            Assert.NotNull(
                user
            );
        }

        [Fact]
        public Task UnsuccesfullLogin()
        {
            return Assert.ThrowsAsync<OdooInvalidLoginException<int>>(
                () => _odooCompany.LoginAsync("zhukovskydenis@gmail.com", "wrongPassword")
            );
        }

        [Fact]
        public async Task GetReceptions()
        {
            var user = await _odooCompany.LoginAsync("zhukovskydenis@gmail.com", "mowmav-vande9-cUsfav");
            Assert.Equal(4, (await _odooCompany.Warehouse.Receptions.ToListAsync()).Count);
        }

        [Fact]
        public async Task GetReceptionGoods()
        {
            var user = await _odooCompany.LoginAsync("zhukovskydenis@gmail.com", "mowmav-vande9-cUsfav");
            var reception = (await _odooCompany.Warehouse.Receptions.ToListAsync()).First() ;
            var goods = reception.Goods.ToListAsync();
            Assert.Equal(4, 4);
        }
    }
}
