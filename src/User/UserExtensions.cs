using System;
using System.Threading.Tasks;
using System.Linq;

namespace Warehouse.Core.User
{
    public static class UserExtensions
    {
        public static async Task<IUser> LoginAsync(this IUsers users, string userName, string password)
        {
            var userslist = await users.With(new UsernameFilter(userName, password)).ToListAsync();
            if (!userslist.Any())
            {
                throw new InvalidOperationException("This user does not exist");
            }
            return userslist.First();
        }
    }
}
