using System;
using System.Collections.Generic;
using System.Linq;

using ProjectManager.Framework.Core.Common.Exceptions;
using ProjectManager.Framework.Core.Commands.Contracts;
using Bytes2you.Validation;

namespace ProjectManager.Framework.Core.Commands.Decorators
{
    public class ValidatableCommand : ICommand
    {
        private readonly ICommand command;

        public ValidatableCommand(ICommand command)
        {
            Guard.WhenArgument(command, "command in Validatable command").IsNull().Throw();

            this.command = command;
        }

        public int ParameterCount
        {
            get
            {
                return this.command.ParameterCount;
            }
        }

        public string Execute(IList<string> parameters)
        {
            Guard.WhenArgument(parameters, "parameters").IsNull().Throw();

            if (this.command != null)
            {
                this.ValidateParameters(parameters);
                return this.command.Execute(parameters);
            }
            else
            {
                throw new NullReferenceException("Command is null");
            }
        }

        private void ValidateParameters(IList<string> parameters)
        {
            if (parameters.Count != this.command.ParameterCount)
            {
                throw new UserValidationException("Invalid command parameters count!");
            }

            if (parameters.Any(x => x == string.Empty))
            {
                throw new UserValidationException("Some of the passed parameters are empty!");
            }
        }
    }
}
