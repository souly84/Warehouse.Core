using System;
using PortaCapena.OdooJsonRpcClient;

namespace Warehouse.Core.Tests
{
    public class OdooUser : IUser
    {
        private readonly OdooClient _client;

        public OdooUser(OdooClient client)
        {
            _client = client;
        }
    }

   
}
