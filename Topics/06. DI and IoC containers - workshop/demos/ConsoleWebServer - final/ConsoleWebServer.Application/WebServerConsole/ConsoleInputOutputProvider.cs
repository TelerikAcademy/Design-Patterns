using System;

namespace ConsoleWebServer.Application.WebServerConsole
{
    public class ConsoleInputOutputProvider : IConsoleInputOutputProvider
    {
        public ConsoleColor BackgroundColor
        {
            get
            {
                return Console.BackgroundColor;
            }

            set
            {
                Console.BackgroundColor = value;
            }
        }

        public ConsoleColor ForegroundColor
        {
            get
            {
                return Console.ForegroundColor;
            }

            set
            {
                Console.ForegroundColor = value;
            }
        }

        public string ReadInput()
        {
            return Console.ReadLine();
        }

        public void WriteOutput(string value)
        {
            Console.WriteLine(value);
        }

        public void ResetColor()
        {
            Console.ResetColor();
        }
    }
}