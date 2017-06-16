using System;

using ProjectManager.Framework.Core.Common.Contracts;

namespace ProjectManager.Framework.Core.Common.Providers
{
    public class ConsoleReader : IReader
    {
        public string ReadLine()
        {
            return Console.ReadLine();
        }
    }
}
