using ProjectManager.Framework.Core.Commands.Contracts;
using ProjectManager.Framework.Core.Common.Exceptions;
using ProjectManager.Framework.Data;
using ProjectManager.Framework.Data.Factories;
using ProjectManager.Framework.Core.Commands.Creational;
using ProjectManager.Framework.Core.Commands.Listing;
using ProjectManager.Framework.Core.Common.Providers;

namespace ProjectManager.Framework.Core.Commands.Factories
{
    public class CommandsFactory : ICommandsFactory
    {
        private readonly IDatabase database;
        private readonly ModelsFactory factory;

        public CommandsFactory()
        {
            this.factory = factory ?? new ModelsFactory(new Validator());
        }

        public ICommand GetCommandFromString(string commandName)
        {
            switch (commandName.ToLower())
            {
                case "createproject":
                    return this.CreateProjectCommand();
                case "createuser":
                    return this.CreateUserCommand();
                case "createtask":
                    return this.CreateTaskCommand();
                case "listprojects":
                    return this.ListProjectCommand();
                case "listprojectdetails":
                    return this.ListProjectDetailsCommand();
                default:
                    throw new UserValidationException("No such command!");
            }
        }

        public ICommand CreateProjectCommand()
        {
            return new CreateProjectCommand(this.factory);
        }

        public ICommand CreateUserCommand()
        {
            return new CreateUserCommand(this.factory);
        }

        public ICommand CreateTaskCommand()
        {
            return new CreateTaskCommand(this.factory);
        }

        public ICommand ListProjectCommand()
        {
            return new ListProjectsCommand();
        }

        public ICommand ListProjectDetailsCommand()
        {
            return new ListProjectDetailsCommand();
        }
    }
}
