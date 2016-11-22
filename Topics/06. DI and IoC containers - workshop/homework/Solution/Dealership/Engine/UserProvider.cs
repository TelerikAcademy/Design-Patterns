using Dealership.Contracts;
using System.Collections.Generic;

namespace Dealership.Engine
{
    public class UserProvider : IUserProvider
    {
        private readonly IList<IUser> users;

        public UserProvider()
        {
            this.users = new List<IUser>();
        }

        public IUser LoggedUser
        {
            get;
            set;
        }

        public IEnumerable<IUser> Users
        {
            get
            {
                return this.users;
            }
        }

        public void AddUser(IUser user)
        {
            this.users.Add(user);
        }
    }
}