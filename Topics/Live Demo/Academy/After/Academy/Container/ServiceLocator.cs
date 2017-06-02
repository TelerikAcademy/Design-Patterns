using Academy.Commands.Contracts;
using Academy.Framework.Core.Contracts;
using Ninject;

namespace Academy.Container
{
    public class ServiceLocator : IServiceLocator
    {
        private readonly IKernel kernel;

        public ServiceLocator(IKernel kernel)
        {
            this.kernel = kernel;
        }

        public ICommand GetCommand(string commandName)
        {
            return this.kernel.Get<ICommand>(commandName);
        }
    }
}