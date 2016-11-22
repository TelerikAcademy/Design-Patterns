using Dealership.Common;
using Dealership.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dealership.CommandHandlers
{
    public abstract class CommandHandlerBase : ICommandHandler
    {
        private ICommandHandlerProcessor Successor { get; set; }

        public virtual string ProcessCommand(ICommand command)
        {
            if (this.CanHandle(command))
            {
                return this.ProccessCommandInternal(command);
            }

            if (this.Successor != null)
            {
                return this.Successor.ProcessCommand(command);
            }

            return string.Format(Constants.InvalidCommand, command.Name);
        }

        public virtual void SetSuccessor(ICommandHandlerProcessor commandHandler)
        {
            this.Successor = commandHandler;
        }

        protected abstract bool CanHandle(ICommand command);

        protected abstract string ProccessCommandInternal(ICommand command);
    }
}