namespace ConsoleWebServer.Framework.Handlers
{
    public interface IHandler
    {
        IHttpResponse HandleRequest(IHttpRequest request);
        void SetSuccessor(IHandler successor);
    }
}