using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dealership.Engine;
using Dealership.Common.Enums;

namespace Dealership.CommandHandlers
{
    public class ShowUsersCommandHandler : CommandHandlerBase
    {
        private const string YouAreNotAnAdmin = "You are not an admin!";

        private readonly IUserProvider userProvider;

        public ShowUsersCommandHandler(IUserProvider userProvider)
        {
            this.userProvider = userProvider;
        }

        protected override bool CanHandle(ICommand command)
        {
            return command.Name == "ShowUsers";
        }

        protected override string ProccessCommandInternal(ICommand command)
        {
            if (this.userProvider.LoggedUser.Role != Role.Admin)
            {
                return YouAreNotAnAdmin;
            }

            var builder = new StringBuilder();
            builder.AppendLine("--USERS--");
            var counter = 1;
            foreach (var user in this.userProvider.Users)
            {
                builder.AppendLine(string.Format("{0}. {1}", counter, user.ToString()));
                counter++;
            }

            return builder.ToString().Trim(); throw new NotImplementedException();
        }
    }
}
