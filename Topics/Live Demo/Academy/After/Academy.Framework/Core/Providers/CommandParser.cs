using Academy.Core.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace Academy.Core.Providers
{
    public class CommandParser : IParser
    {
        public IList<string> ParseParameters(string fullCommand)
        {
            var commandParts = fullCommand.Split(' ').ToList();
            commandParts.RemoveAt(0);

            if (commandParts.Count() == 0)
            {
                return new List<string>();
            }

            return commandParts;
        }
    }
}