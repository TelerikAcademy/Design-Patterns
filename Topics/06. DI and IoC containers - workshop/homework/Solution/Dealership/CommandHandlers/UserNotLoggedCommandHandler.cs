using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dealership.Engine;

namespace Dealership.CommandHandlers
{
    public class UserNotLoggedCommandHandler : CommandHandlerBase
    {
        private const string UserNotLogged = "You are not logged! Please login first!";

        private readonly IUserProvider userProvider;

        public UserNotLoggedCommandHandler(IUserProvider userProvider)
        {
            this.userProvider = userProvider;
        }

        protected override bool CanHandle(ICommand command)
        {
            return command.Name != "RegisterUser" && command.Name != "Login" && this.userProvider.LoggedUser == null;
        }

        protected override string ProccessCommandInternal(ICommand command)
        {
            return UserNotLogged;
        }
    }
}