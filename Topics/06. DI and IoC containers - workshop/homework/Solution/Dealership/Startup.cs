using Dealership.CommandHandlers;
using Dealership.Engine;
using Ninject;
using System;
using System.IO;

namespace Dealership
{
    public class Startup
    {
        public static void Main()
        {
            IKernel kernel = new StandardKernel(new DealershipModule());

            IEngine engine = kernel.Get<IEngine>();
            engine.Start();
        }
    }
}