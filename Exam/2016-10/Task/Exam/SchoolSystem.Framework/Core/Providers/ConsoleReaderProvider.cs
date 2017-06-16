using System;
using SchoolSystem.Framework.Core.Contracts;

namespace SchoolSystem.Framework.Core.Providers
{
    public class ConsoleReaderProvider : IReader
    {
        public string ReadLine()
        {
            return Console.ReadLine();
        }
    }
}
