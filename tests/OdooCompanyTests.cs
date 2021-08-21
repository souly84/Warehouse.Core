using System.Linq;
using System.Threading.Tasks;
using PortaCapena.OdooJsonRpcClient;
using PortaCapena.OdooJsonRpcClient.Models;
using Warehouse.Core.Tests.Extensions;
using Xunit;
using Xunit.Abstractions;

namespace Warehouse.Core.Tests
{
    public class OdooCompanyTests
    {
        private static string _odooCompanyUri = "https://testreception1.odoo.com";
        private static string _odooDb = "testreception1";
        private string _userName = "zhukovskydenis@gmail.com";
        private string _userPassword = "fuqNu1-tywhaq-gubwig";
        private OdooCompany _odooCompany = new OdooCompany(
            _odooCompanyUri,
            _odooDb
        );
        private readonly ITestOutputHelper _output;

        public OdooCompanyTests(ITestOutputHelper output)
        {
            _output = output;
        }

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
            Assert.NotEmpty(
                await _odooCompany.Warehouse.Receptions.ToListAsync()
            );
        }

        [Fact]
        public async Task GetReceptionGoods()
        {
            await _odooCompany.LoginAsync(_userName, _userPassword);
            var receptions = await _odooCompany.Warehouse.Receptions.ToListAsync();
            Assert.NotEmpty(
                await receptions.First().Goods.ToListAsync()
            );
        }

        [Fact(Skip = "Only for manual run")]
        public async Task ReceptionsDto()
        {
            var client = new OdooClient(
                new OdooConfig(
                   _odooCompanyUri,
                   _odooDb,
                   _userName,
                   _userPassword
                )
            );
            await client.LoginAsync();
            _output.WriteLine(await client.DotNetModel(new OdooStockGoodDto()));
        }
    }
}
