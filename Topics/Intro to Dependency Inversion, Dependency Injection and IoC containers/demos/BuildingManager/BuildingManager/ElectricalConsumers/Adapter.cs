using BuildingManager.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingManager.ElectricalConsumers
{
    public class Adapter : IElectricalDevice
    {
        private readonly IAmericanElectricalDevice americanConsumer;

        public Adapter(IAmericanElectricalDevice americanConsumer)
        {
            this.americanConsumer = americanConsumer;
        }

        public void ConsumeElectricity(double electricity)
        {
            this.americanConsumer.ConsumeAmericanElectricity(electricity);
        }

        public override string ToString()
        {
            return string.Format("Adapter with:\n{0}", this.americanConsumer);
        }
    }
}
