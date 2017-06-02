using Academy.Commands.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academy.Core.Factories
{
    public interface ICommandFactory
    {
        ICommand GetCommand(string fullCommand);
    }
}
