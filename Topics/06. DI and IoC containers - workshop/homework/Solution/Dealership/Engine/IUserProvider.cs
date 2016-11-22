using Dealership.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dealership.Engine
{
    public interface IUserProvider
    {
        IUser LoggedUser { get; set; }

        IEnumerable<IUser> Users { get; }

        void AddUser(IUser user);
    }
}