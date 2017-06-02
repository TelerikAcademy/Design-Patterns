using Academy.Core.Contracts;
using System;

namespace Academy.Core.Providers
{
    public class ConsoleLogger : ILogger
    {
        public void Log(string message)
        {
            Console.WriteLine(message);
        }
    }
}