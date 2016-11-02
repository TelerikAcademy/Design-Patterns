namespace ConsoleWebServer.Framework
{
    using System;

    public class ParserException : Exception
    {
        public ParserException(string message)
            : base(message)
        {
        }
    }
}
