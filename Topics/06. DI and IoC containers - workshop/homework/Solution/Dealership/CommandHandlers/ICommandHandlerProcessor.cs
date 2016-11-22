using Dealership.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dealership.CommandHandlers
{
    public interface ICommandHandlerProcessor
    {
        string ProcessCommand(ICommand command);
    }
}