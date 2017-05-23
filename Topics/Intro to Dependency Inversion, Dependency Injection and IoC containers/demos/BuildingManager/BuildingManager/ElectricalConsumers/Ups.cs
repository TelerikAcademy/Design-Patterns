using BuildingManager.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingManager.ElectricalConsumers
{
    public class Ups : IElectricalDevice
    {
        private const double MaxReservedCapacity = 50;
        private const double PassedElectricity = 5;

        private readonly IElectricalDevice electricalConsumer;

        public Ups(IElectricalDevice electricalConsumer)
        {
            this.electricalConsumer = electricalConsumer;
        }

        public double ReservedCapacity
        {
            get;
            private set;
        }

        public void ConsumeElectricity(double electricity)
        {
            double electricityToPass = electricity;

            if (electricityToPass > 0)
            {
                this.ReservedCapacity = Math.Min(this.ReservedCapacity + electricityToPass, MaxReservedCapacity);
            }
            else if (this.ReservedCapacity > 0)
            {
                electricityToPass = Math.Min(PassedElectricity, this.ReservedCapacity);
                this.ReservedCapacity -= electricityToPass;
            }

            this.electricalConsumer.ConsumeElectricity(electricityToPass);
        }

        public override string ToString()
        {
            return string.Format("Ups with reserved capacity {0}% and device:\n{1}", (this.ReservedCapacity / MaxReservedCapacity) * 100, this.electricalConsumer);
        }
    }
}
