namespace ConsoleWebServer.Framework.Handlers
{
    public interface IHandlerFactory
    {
        Handler CreateAndAttachHandlers();
    }
}
