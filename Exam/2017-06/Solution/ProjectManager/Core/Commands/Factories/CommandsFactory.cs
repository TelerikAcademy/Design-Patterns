using System;
using ProjectManager.Framework.Core.Commands.Contracts;
using ProjectManager.Framework.Core.Common.Exceptions;
using Ninject;
using Ninject.Syntax;
using Bytes2you.Validation;

namespace ProjectManager.Framework.Core.Commands.Factories
{
    public class CommandsFactory : ICommandsFactory
    {
        private readonly IResolutionRoot root;

        public CommandsFactory(IResolutionRoot root)
        {
            Guard.WhenArgument(root, "Resolution root in CommandsFactory").IsNull().Throw();
            this.root = root;
        }

        public ICommand GetCommandFromString(string commandName)
        {
            Guard.WhenArgument(commandName, "commandName in CommandsFactory").IsNull().Throw();

            try
            {
                var cmd = this.root.Get<ICommand>(commandName);
                return cmd;
            }
            catch (Exception)
            {
                throw new UserValidationException("No such command");
            }
        }
    }
}
