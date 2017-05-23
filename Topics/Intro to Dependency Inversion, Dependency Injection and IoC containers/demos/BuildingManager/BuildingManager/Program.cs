using BuildingManager.Interfaces;
using BuildingManager.NinjectModules;
using Ninject;
using System;

namespace BuildingManager
{
    class Program
    {
        static void Main(string[] args)
        {
            IKernel kernel = new StandardKernel(new BuildingManagerModule());
            
            IElectricalDevice consumer = kernel.Get<IElectricalDevice>(BuildingManagerModule.HighElectricityDefenderName);

            consumer.ConsumeElectricity(5);

            Console.WriteLine("-------------");
            Console.WriteLine(consumer);
            Console.WriteLine("-------------");

            consumer.ConsumeElectricity(200);

            Console.WriteLine("-------------");
            Console.WriteLine(consumer);
            Console.WriteLine("-------------");
        }
    }
}