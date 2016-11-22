using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dealership.CommandHandlers
{
    public interface ICommandHandler : ICommandHandlerProcessor
    {
        void SetSuccessor(ICommandHandlerProcessor commandHandler);
    }
}