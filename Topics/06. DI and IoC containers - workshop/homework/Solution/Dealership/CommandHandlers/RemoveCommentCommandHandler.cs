using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dealership.Engine;
using Dealership.Common;

namespace Dealership.CommandHandlers
{
    public class RemoveCommentCommandHandler : CommandHandlerBase
    {
        private const string RemovedCommentDoesNotExist = "Cannot remove comment! The comment does not exist!";
        private const string CommentRemovedSuccessfully = "{0} removed comment successfully!";

        private readonly IUserProvider userProvider;

        public RemoveCommentCommandHandler(IUserProvider userProvider)
        {
            this.userProvider = userProvider;
        }

        protected override bool CanHandle(ICommand command)
        {
            return command.Name == "RemoveComment";
        }

        protected override string ProccessCommandInternal(ICommand command)
        {
            var vehicleIndex = int.Parse(command.Parameters[0]) - 1;
            var commentIndex = int.Parse(command.Parameters[1]) - 1;
            var username = command.Parameters[2];

            return this.RemoveComment(vehicleIndex, commentIndex, username);
        }

        private string RemoveComment(int vehicleIndex, int commentIndex, string username)
        {
            var user = this.userProvider.Users.FirstOrDefault(u => u.Username == username);

            if (user == null)
            {
                return string.Format(Constants.NoSuchUser, username);
            }

            Validator.ValidateRange(vehicleIndex, 0, user.Vehicles.Count, Constants.RemovedVehicleDoesNotExist);
            Validator.ValidateRange(commentIndex, 0, user.Vehicles[vehicleIndex].Comments.Count, RemovedCommentDoesNotExist);

            var vehicle = user.Vehicles[vehicleIndex];
            var comment = user.Vehicles[vehicleIndex].Comments[commentIndex];

            this.userProvider.LoggedUser.RemoveComment(comment, vehicle);
            
            return string.Format(CommentRemovedSuccessfully, this.userProvider.LoggedUser.Username);
        }
    }
}