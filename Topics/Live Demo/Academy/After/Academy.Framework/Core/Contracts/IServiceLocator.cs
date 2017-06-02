using Academy.Commands.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academy.Framework.Core.Contracts
{
    public interface IServiceLocator
    {
        ICommand GetCommand(string commandName);
    }
}
