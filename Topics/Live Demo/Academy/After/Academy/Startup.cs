using Academy.Container;
using Academy.Core.Contracts;
using Ninject;

namespace Academy
{
    public class Startup
    {
        public static void Main(string[] args)
        {
            IKernel kernel = new StandardKernel(new AcademyNinjectModule());

            IEngine engine = kernel.Get<IEngine>();
            engine.Start();
        }
    }
}