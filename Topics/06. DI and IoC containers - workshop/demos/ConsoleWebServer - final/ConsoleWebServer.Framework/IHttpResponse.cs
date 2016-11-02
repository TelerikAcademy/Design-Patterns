namespace ConsoleWebServer.Framework
{
    public interface IHttpResponse : IHttpMessage
    {
        string Body { get; }
    }
}