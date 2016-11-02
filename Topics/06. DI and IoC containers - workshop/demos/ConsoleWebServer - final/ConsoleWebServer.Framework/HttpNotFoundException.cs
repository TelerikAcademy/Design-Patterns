using System;

namespace ConsoleWebServer.Framework
{
    public class HttpNotFoundException : Exception
    {
        public HttpNotFoundException(string message)
            : base(message)
        {
        }
    }
}