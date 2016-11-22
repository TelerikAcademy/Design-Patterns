using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dealership.Engine;
using Dealership.Factories;
using Dealership.Common;

namespace Dealership.CommandHandlers
{
    public class AddCommentCommandHandler : CommandHandlerBase
    {
        private const string VehicleDoesNotExist = "The vehicle does not exist!";
        private const string CommentAddedSuccessfully = "{0} added comment successfully!";

        private readonly IDealershipFactory dealershipFactory;
        private readonly IUserProvider userProvider;

        public AddCommentCommandHandler(IDealershipFactory dealershipFactory, IUserProvider userProvider)
        {
            this.dealershipFactory = dealershipFactory;
            this.userProvider = userProvider;
        }

        protected override bool CanHandle(ICommand command)
        {
            return command.Name == "AddComment";
        }

        protected override string ProccessCommandInternal(ICommand command)
        {
            var content = command.Parameters[0];
            var author = command.Parameters[1];
            var vehicleIndex = int.Parse(command.Parameters[2]) - 1;

            return this.AddComment(content, vehicleIndex, author);
        }

        private string AddComment(string content, int vehicleIndex, string author)
        {
            var comment = this.dealershipFactory.CreateComment(content);
            comment.Author = this.userProvider.LoggedUser.Username;
            var user = this.userProvider.Users.FirstOrDefault(u => u.Username == author);

            if (user == null)
            {
                return string.Format(Constants.NoSuchUser, author);
            }

            Validator.ValidateRange(vehicleIndex, 0, user.Vehicles.Count, VehicleDoesNotExist);

            var vehicle = user.Vehicles[vehicleIndex];

            this.userProvider.LoggedUser.AddComment(comment, vehicle);

            return string.Format(CommentAddedSuccessfully, this.userProvider.LoggedUser.Username);
        }
    }
}