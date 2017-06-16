using System.Collections.Generic;

namespace ProjectManager.Framework.Core.Commands.Contracts
{
    public interface ICommand
    {
        string Execute(IList<string> parameters);

        int ParameterCount { get; }
    }
}
