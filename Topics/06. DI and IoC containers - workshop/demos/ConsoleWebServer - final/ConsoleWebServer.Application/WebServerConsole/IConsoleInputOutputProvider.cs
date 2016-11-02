using System;

namespace ConsoleWebServer.Application.WebServerConsole
{
    public interface IConsoleInputOutputProvider : IInputOutputProvider
    {
        ConsoleColor BackgroundColor { get; set; }

        ConsoleColor ForegroundColor { get; set; }

        void ResetColor();
    }
}