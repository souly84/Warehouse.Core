using System;
using System.Collections.Generic;
using Warehouse.Core.Core;

namespace Warehouse.Core.User
{
    public class UsernameFilter : IFilter
    {
        private readonly string _username;
        private readonly string _password;

        public UsernameFilter(string username, string password)
        {
            _username = username;
            _password = password;
        }
        public Dictionary<string, object> ToParams()
        {
            return new Dictionary<string, object> { { "username", _username }, {"password", _password } };
        }
    }
}
