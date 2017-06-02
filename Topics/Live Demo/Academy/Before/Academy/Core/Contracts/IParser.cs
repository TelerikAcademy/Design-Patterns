using Academy.Commands.Contracts;
using System.Collections.Generic;

namespace Academy.Core.Contracts
{
    public interface IParser
    {
        ICommand ParseCommand(string fullCommand);

        IList<string> ParseParameters(string fullCommand);
    }
}
