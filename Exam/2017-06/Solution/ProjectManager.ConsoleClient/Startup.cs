using Ninject;
using ProjectManager.Configs;
using ProjectManager.Framework.Core.Common.Contracts;

namespace ProjectManager.ConsoleClient
{
    public class Startup
    {
        public static void Main()
        {
            var kernel = new StandardKernel(new NinjectManagerModule());

            var engine = kernel.Get<IEngine>();

            engine.Start();
        }
    }
}
