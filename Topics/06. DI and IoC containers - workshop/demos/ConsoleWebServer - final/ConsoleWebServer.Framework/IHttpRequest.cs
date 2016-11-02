namespace ConsoleWebServer.Framework
{
    public interface IHttpRequest : IHttpMessage
    {
        string Uri { get; }

        string Method { get; }

        IActionDescriptor Action { get; }
    }
}