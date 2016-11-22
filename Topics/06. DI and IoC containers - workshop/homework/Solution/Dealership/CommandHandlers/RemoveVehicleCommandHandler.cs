using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dealership.Engine;
using Dealership.Common;

namespace Dealership.CommandHandlers
{
    public class RemoveVehicleCommandHandler : CommandHandlerBase
    {
        private const string VehicleRemovedSuccessfully = "{0} removed vehicle successfully!";

        private readonly IUserProvider userProvider;

        public RemoveVehicleCommandHandler(IUserProvider userProvider)
        {
            this.userProvider = userProvider;
        }

        protected override bool CanHandle(ICommand command)
        {
            return command.Name == "RemoveVehicle";
        }

        protected override string ProccessCommandInternal(ICommand command)
        {
            var vehicleIndex = int.Parse(command.Parameters[0]) - 1;

            return this.RemoveVehicle(vehicleIndex);
        }

        private string RemoveVehicle(int vehicleIndex)
        {
            Validator.ValidateRange(vehicleIndex, 0, this.userProvider.LoggedUser.Vehicles.Count, Constants.RemovedVehicleDoesNotExist);

            var vehicle = this.userProvider.LoggedUser.Vehicles[vehicleIndex];

            this.userProvider.LoggedUser.RemoveVehicle(vehicle);

            return string.Format(VehicleRemovedSuccessfully, this.userProvider.LoggedUser.Username);
        }
    }
}