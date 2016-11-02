using ConsoleWebServer.Framework;
using System;

namespace ConsoleWebServer.Application.WebServerConsole
{
    public class ConsoleWebServer : WebServerBase<IConsoleInputOutputProvider>
    {
        public ConsoleWebServer(IResponseProvider responseProvider, IConsoleInputOutputProvider inputOutputProvider)
            : base(responseProvider, inputOutputProvider)
        {
        }

        protected override void WriteOutput(string value)
        {
            this.InputOutputProvider.ForegroundColor = ConsoleColor.DarkGray;
            this.InputOutputProvider.WriteOutput(value);
            this.InputOutputProvider.ResetColor();
        }
    }
}