using BuildingManager.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingManager.ElectricalConsumers
{
    public class Laptop : IElectricalDevice
    {
        private const double MaxCapacity = 100;

        public double ElectricityCapacity
        {
            get;
            private set;
        }

        public void ConsumeElectricity(double electricity)
        {
            this.ElectricityCapacity = Math.Min(this.ElectricityCapacity + electricity, MaxCapacity);
        }

        public override string ToString()
        {
            return string.Format("Laptop with capacity: {0}%", (this.ElectricityCapacity / MaxCapacity) * 100);
        }
    }
}
