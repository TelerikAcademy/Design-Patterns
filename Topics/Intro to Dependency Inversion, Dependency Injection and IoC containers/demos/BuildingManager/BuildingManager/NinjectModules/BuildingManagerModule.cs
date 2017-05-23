using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject;
using BuildingManager.Interfaces;
using BuildingManager.ElectricalConsumers;

namespace BuildingManager.NinjectModules
{
    public class BuildingManagerModule : NinjectModule
    {
        public const string LaptopName = "Laptop";
        public const string AdapterName = "Adapter";
        public const string UpsName = "Ups";
        public const string PowerStripName = "PowerStrip";
        public const string HighElectricityDefenderName = "HighElectricityDefender";
        public const string AmericanLaptopName = "AmericanLaptop";

        public override void Load()
        {
            var laptop = this.Bind<IElectricalDevice>().To<Laptop>().Named(LaptopName);
            var adapter = this.Bind<IElectricalDevice>().To<Adapter>().Named(AdapterName);
            var ups = this.Bind<IElectricalDevice>().To<Ups>().Named(UpsName);
            var powerStrip = this.Bind<IElectricalDevice>().To<PowerStrip>().Named(PowerStripName);
            var highElectricityDefender = this.Bind<IElectricalDevice>().To<HighElectricityDefender>().Named(HighElectricityDefenderName);
            var americanLaptop = this.Bind<IAmericanElectricalDevice>().To<AmericanLaptop>().Named(AmericanLaptopName);

            adapter.WithConstructorArgument(this.Kernel.Get<IAmericanElectricalDevice>(AmericanLaptopName));
            powerStrip.WithConstructorArgument<IEnumerable<IElectricalDevice>>(new List<IElectricalDevice>()
            {
                this.Kernel.Get<IElectricalDevice>(LaptopName),
                this.Kernel.Get<IElectricalDevice>(AdapterName)
            });
            ups.WithConstructorArgument(this.Kernel.Get<IElectricalDevice>(PowerStripName));
            highElectricityDefender.WithConstructorArgument(this.Kernel.Get<IElectricalDevice>(UpsName));
        }
    }
}