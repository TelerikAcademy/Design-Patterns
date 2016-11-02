namespace ConsoleWebServer.Application
{
    using ConsoleWebServer.Framework;
    using ConsoleWebServer.Framework.Handlers;

    public static class Startup
    {
        public static void Main()
        {
            IHandlerFactory handlerFactory = new HandlerFactory();
            IResponseProvider responseProvider = new ResponseProvider(handlerFactory);
            var webSever = new WebServerConsole(responseProvider);
            webSever.Start();
        }
    }
}
