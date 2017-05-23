using BuildingManager.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingManager.ElectricalConsumers
{
    public class HighElectricityDefender : IElectricalDevice
    {
        private const double MaxElectricityAllowed = 100;

        private readonly IElectricalDevice electricalConsumer;

        public HighElectricityDefender(IElectricalDevice electricalConsumer)
        {
            this.electricalConsumer = electricalConsumer;
        }

        public void ConsumeElectricity(double electricity)
        {
            if (electricity <= MaxElectricityAllowed)
            {
                this.electricalConsumer.ConsumeElectricity(electricity);
            }
        }

        public override string ToString()
        {
            return string.Format("Defender with:\n{0}", this.electricalConsumer);
        }
    }
}
