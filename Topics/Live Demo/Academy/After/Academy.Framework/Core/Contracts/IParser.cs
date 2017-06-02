using Academy.Commands.Contracts;
using System.Collections.Generic;

namespace Academy.Core.Contracts
{
    public interface IParser
    {
        IList<string> ParseParameters(string fullCommand);
    }
}
