namespace ConsoleWebServer.Framework.Handlers
{
    public class HandlerFactory : IHandlerFactory
    {
        public Handler CreateAndAttachHandlers()
        {
            var headHandler = new HeadHandler();
            var optionsHandler = new OptionsHandler();
            var fileHandler = new StaticFileHandler();
            var controllerHandler = new ControllerHandler();

            headHandler.SetSuccessor(optionsHandler);
            optionsHandler.SetSuccessor(fileHandler);
            fileHandler.SetSuccessor(controllerHandler);

            return headHandler;
        }
    }
}
