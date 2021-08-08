using System.Threading.Tasks;
using PortaCapena.OdooJsonRpcClient;
using PortaCapena.OdooJsonRpcClient.Models;
using Warehouse.Core.Tests.Extensions;
using Xunit;

namespace Warehouse.Core.Tests
{
    public class Tests
    {
        private string _userName = "zhukovskydenis@gmail.com";
        private string _userPassword = "mowmav-vande9-cUsfav";
        private OdooCompany _odooCompany = new OdooCompany(
            "https://testreception.odoo.com",
            "testreception"
        );
       
        [Fact]
        public async Task SuccesfullLogin()
        {
            Assert.NotNull(
                await _odooCompany.LoginAsync(_userName, _userPassword)
            );
        }

        [Fact]
        public Task UnsuccesfullLogin()
        {
            return Assert.ThrowsAsync<OdooInvalidLoginException<int>>(
                () => _odooCompany.LoginAsync("wrongEmail@gmail.com", "wrongPassword")
            );
        }

        [Fact]
        public async Task GetReceptions()
        {
            await _odooCompany.LoginAsync(_userName, _userPassword);
            Assert.Equal(
                4,
                (await _odooCompany.Warehouse.Receptions.ToListAsync()).Count
            );
        }

        [Fact(Skip = "Only for manual run")]
        public async Task ReceptionsDto()
        {
            var client = new OdooClient(
                new OdooConfig(
                   "https://testreception.odoo.com",
                    "testreception",
                    _userName,
                    _userPassword
                )
            );
            await client.LoginAsync();
            var aaa = await client.DotNetModel(new OdooStockGoodDto());
        }
    }
}
