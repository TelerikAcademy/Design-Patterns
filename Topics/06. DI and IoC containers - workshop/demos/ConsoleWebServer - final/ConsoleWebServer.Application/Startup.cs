using ConsoleWebServer.Application.WebServerConsole;
using Ninject;

namespace ConsoleWebServer.Application
{
    public static class Startup
    {
        public static void Main()
        {
            IKernel kernel = new StandardKernel(new WebServerModule());

            IWebServerConsole webServer = kernel.Get<IWebServerConsole>();
            webServer.Start();
        }
    }
}