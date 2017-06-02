using Academy.Commands.Contracts;
using Academy.Framework.Core.Contracts;

namespace Academy.Core.Factories
{
    public class CommandFactory : ICommandFactory
    {
        private readonly IServiceLocator serviceLocator;

        public CommandFactory(IServiceLocator serviceLocator)
        {
            this.serviceLocator = serviceLocator;
        }

        public ICommand GetCommand(string fullCommand)
        {
            string commandName = fullCommand.Split(' ')[0];

            return this.serviceLocator.GetCommand(commandName);
        }
    }
}