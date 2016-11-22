using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dealership.Engine;
using Dealership.Common.Enums;
using Dealership.Contracts;
using Dealership.Factories;

namespace Dealership.CommandHandlers
{
    public class AddVehicleCommandHandler : CommandHandlerBase
    {
        private const string VehicleAddedSuccessfully = "{0} added vehicle successfully!";

        private readonly IDealershipFactory dealershipFactory;
        private readonly IUserProvider userProvider;

        public AddVehicleCommandHandler(IDealershipFactory dealershipFactory, IUserProvider userProvider)
        {
            this.dealershipFactory = dealershipFactory;
            this.userProvider = userProvider;
        }

        protected override bool CanHandle(ICommand command)
        {
            return command.Name == "AddVehicle";
        }

        protected override string ProccessCommandInternal(ICommand command)
        {
            var type = command.Parameters[0];
            var make = command.Parameters[1];
            var model = command.Parameters[2];
            var price = decimal.Parse(command.Parameters[3]);
            var additionalParam = command.Parameters[4];

            var typeEnum = (VehicleType)Enum.Parse(typeof(VehicleType), type, true);

            return this.AddVehicle(typeEnum, make, model, price, additionalParam);
        }

        private string AddVehicle(VehicleType type, string make, string model, decimal price, string additionalParam)
        {
            IVehicle vehicle = null;

            if (type == VehicleType.Car)
            {
                vehicle = this.dealershipFactory.CreateCar(make, model, price, int.Parse(additionalParam));
            }
            else if (type == VehicleType.Motorcycle)
            {
                vehicle = this.dealershipFactory.CreateMotorcycle(make, model, price, additionalParam);
            }
            else if (type == VehicleType.Truck)
            {
                vehicle = this.dealershipFactory.CreateTruck(make, model, price, int.Parse(additionalParam));
            }

            this.userProvider.LoggedUser.AddVehicle(vehicle);

            return string.Format(VehicleAddedSuccessfully, this.userProvider.LoggedUser.Username);
        }
    }
}