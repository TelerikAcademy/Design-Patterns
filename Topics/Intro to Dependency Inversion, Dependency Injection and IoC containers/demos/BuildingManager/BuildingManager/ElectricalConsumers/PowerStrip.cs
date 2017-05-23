using BuildingManager.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingManager.ElectricalConsumers
{
    public class PowerStrip : IElectricalDevice
    {
        private readonly IEnumerable<IElectricalDevice> electricalConsumers;

        public PowerStrip(IEnumerable<IElectricalDevice> electricalConsumers)
        {
            this.electricalConsumers = electricalConsumers;
        }

        public void ConsumeElectricity(double electricity)
        {
            foreach (IElectricalDevice consumer in this.electricalConsumers)
            {
                consumer.ConsumeElectricity(electricity);
            }
        }

        public override string ToString()
        {
            return string.Format("Power strip with devices:\n{0}", string.Join("\n", this.electricalConsumers));
        }
    }
}
