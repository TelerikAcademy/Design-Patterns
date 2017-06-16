using System.Collections.Generic;

using Bytes2you.Validation;
using ProjectManager.Framework.Core.Commands.Contracts;
using ProjectManager.Framework.Data;

namespace ProjectManager.Framework.Core.Commands.Abstracts
{
    public abstract class Command : ICommand
    {
        protected readonly IDatabase database;

        public Command(IDatabase database)
        {
            Guard.WhenArgument(database, "CreateProjectCommand Database").IsNull().Throw();

            this.database = database;
        }

        protected IDatabase Database
        {
            get
            {
                return this.database;
            }
        }

        public abstract int ParameterCount
        {
            get;
        }

        public abstract string Execute(IList<string> parameters);
    }
}
