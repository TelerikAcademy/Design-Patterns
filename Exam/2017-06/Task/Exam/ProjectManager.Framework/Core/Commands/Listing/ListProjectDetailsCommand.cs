using System.Collections.Generic;

using ProjectManager.Framework.Core.Commands.Abstracts;
using ProjectManager.Framework.Core.Commands.Contracts;
using ProjectManager.Framework.Core.Common.Exceptions;
using ProjectManager.Framework.Data;

namespace ProjectManager.Framework.Core.Commands.Listing
{
    public sealed class ListProjectDetailsCommand : Command, ICommand
    {
        private const int ParameterCountConstant = 1;

        public ListProjectDetailsCommand()
        {
        }

        public override int ParameterCount
        {
            get
            {
                return ParameterCountConstant;
            }
        }

        public override string Execute(IList<string> parameters)
        {
            this.ValidateParameters(parameters);

            var projectId = int.Parse(parameters[0]);
            if (this.Database.Projects.Count <= projectId || projectId < 0)
            {
                throw new UserValidationException("The project is not present in the database");
            }

            var project = this.Database.Projects[projectId];

            return project.ToString();
        }
    }
}
