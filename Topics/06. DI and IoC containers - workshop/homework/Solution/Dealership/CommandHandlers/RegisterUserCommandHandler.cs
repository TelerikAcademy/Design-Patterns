using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dealership.Engine;
using Dealership.Common.Enums;
using Dealership.Factories;
using Dealership.Common;

namespace Dealership.CommandHandlers
{
    public class RegisterUserCommandHandler : CommandHandlerBase
    {
        private const string UserAlreadyExist = "User {0} already exist. Choose a different username!";
        private const string UserRegisterеd = "User {0} registered successfully!";

        private readonly IUserProvider userProvider;
        private readonly IDealershipFactory dealershipFactory;

        public RegisterUserCommandHandler(IUserProvider userProvider, IDealershipFactory dealershipFactory)
        {
            this.userProvider = userProvider;
            this.dealershipFactory = dealershipFactory;
        }

        protected override bool CanHandle(ICommand command)
        {
            return command.Name == "RegisterUser";
        }

        protected override string ProccessCommandInternal(ICommand command)
        {
            var username = command.Parameters[0];
            var firstName = command.Parameters[1];
            var lastName = command.Parameters[2];
            var password = command.Parameters[3];

            var role = Role.Normal;

            if (command.Parameters.Count > 4)
            {
                role = (Role)Enum.Parse(typeof(Role), command.Parameters[4]);
            }

            return this.RegisterUser(username, firstName, lastName, password, role);
        }

        private string RegisterUser(string username, string firstName, string lastName, string password, Role role)
        {
            if (this.userProvider.LoggedUser != null)
            {
                return string.Format(Constants.UserLoggedInAlready, this.userProvider.LoggedUser.Username);
            }

            if (this.userProvider.Users.Any(u => u.Username.ToLower() == username.ToLower()))
            {
                return string.Format(UserAlreadyExist, username);
            }

            var user = this.dealershipFactory.CreateUser(username, firstName, lastName, password, role);
            this.userProvider.LoggedUser = user;
            this.userProvider.AddUser(user);

            return string.Format(UserRegisterеd, username);
        }
    }
}