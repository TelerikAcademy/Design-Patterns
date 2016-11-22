using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dealership.Engine;
using Dealership.Common;

namespace Dealership.CommandHandlers
{
    public class LoginCommandHandler : CommandHandlerBase
    {
        private const string UserLoggedIn = "User {0} successfully logged in!";
        private const string WrongUsernameOrPassword = "Wrong username or password!";

        private readonly IUserProvider userProvider;

        public LoginCommandHandler(IUserProvider userProvider)
        {
            this.userProvider = userProvider;
        }

        protected override bool CanHandle(ICommand command)
        {
            return command.Name == "Login";
        }

        protected override string ProccessCommandInternal(ICommand command)
        {
            var username = command.Parameters[0];
            var password = command.Parameters[1];

            return this.Login(username, password);
        }

        private string Login(string username, string password)
        {
            if (this.userProvider.LoggedUser != null)
            {
                return string.Format(Constants.UserLoggedInAlready, this.userProvider.LoggedUser.Username);
            }

            var userFound = this.userProvider.Users.FirstOrDefault(u => u.Username.ToLower() == username.ToLower());

            if (userFound != null && userFound.Password == password)
            {
                this.userProvider.LoggedUser = userFound;
                return string.Format(UserLoggedIn, username);
            }

            return WrongUsernameOrPassword;
        }
    }
}