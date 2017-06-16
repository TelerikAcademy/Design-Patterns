using Bytes2you.Validation;
using ProjectManager.Framework.Core.Commands.Contracts;
using ProjectManager.Framework.Data;
using ProjectManager.Framework.Data.Factories;

namespace ProjectManager.Framework.Core.Commands.Abstracts
{
    public abstract class CreationalCommand : Command, ICommand
    {
        private readonly IModelsFactory factory;

        public CreationalCommand(IDatabase database, IModelsFactory factory) 
            : base(database)
        {
            Guard.WhenArgument(factory, "CreateProjectCommand ModelsFactory").IsNull().Throw();
            this.factory = factory;
        }

        protected IModelsFactory Factory
        {
            get
            {
                return this.factory;
            }
        }
    }
}
