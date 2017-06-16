using ProjectManager.Framework.Core.Commands.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Framework.Core.Commands.Decorators
{
    public class CacheableCommand : ICommand
    {
        private readonly ICommand command;

        public CacheableCommand(ICommand command)
        {
            this.command = command;
        }

        public int ParameterCount
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public string Execute(IList<string> parameters)
        {
            throw new NotImplementedException();
        }
    }
}
