using System;
using System.Threading.Tasks;
using PortaCapena.OdooJsonRpcClient;
using PortaCapena.OdooJsonRpcClient.Models;

namespace Warehouse.Core.Tests
{
    public class OdooCompany : ICompany
    {
        private OdooClient _client;

        public OdooCompany(string companyUri, string dbName) : this(new OdooClient(new OdooConfig(companyUri, dbName, "", "")))
        {
        }

        public OdooCompany(OdooClient client)
        {
            _client = client;
        }
        

        public ICustomers Customers => throw new NotImplementedException();

        public IUsers Users => throw new NotImplementedException();

        public IWarehouse Warehouse =>  new OdooWarehouse(_client);

        public async Task<IUser> LoginAsync(string userName, string password)
        {
            _client = new OdooClient(
                new OdooConfig(_client.Config.ApiUrl, _client.Config.DbName, userName, password)
                );

            var result = await _client.LoginAsync();
            if (result.Failed)
            {
                throw new OdooInvalidLoginException<int>("Login failed: Login/Password can be wrong", result);
            }

            return new OdooUser(_client);
        }
    }
}
