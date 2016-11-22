using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dealership.Engine;

namespace Dealership.CommandHandlers
{
    public class LogoutCommandHandler : CommandHandlerBase
    {
        private const string UserLoggedOut = "You logged out!";

        private readonly IUserProvider userProvider;

        public LogoutCommandHandler(IUserProvider userProvider)
        {
            this.userProvider = userProvider;
        }

        protected override bool CanHandle(ICommand command)
        {
            return command.Name == "Logout";
        }

        protected override string ProccessCommandInternal(ICommand command)
        {
            this.userProvider.LoggedUser = null;

            return UserLoggedOut;
        }
    }
}