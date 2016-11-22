using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dealership.Engine;
using Dealership.Common;

namespace Dealership.CommandHandlers
{
    public class ShowVehiclesCommandHandler : CommandHandlerBase
    {
        private readonly IUserProvider userProvider;

        public ShowVehiclesCommandHandler(IUserProvider userProvider)
        {
            this.userProvider = userProvider;
        }

        protected override bool CanHandle(ICommand command)
        {
            return command.Name == "ShowVehicles";
        }

        protected override string ProccessCommandInternal(ICommand command)
        {
            var username = command.Parameters[0];

            return this.ShowUserVehicles(username);
        }

        private string ShowUserVehicles(string username)
        {
            var user = this.userProvider.Users.FirstOrDefault(u => u.Username.ToLower() == username.ToLower());

            if (user == null)
            {
                return string.Format(Constants.NoSuchUser, username);
            }

            return user.PrintVehicles();
        }
    }
}