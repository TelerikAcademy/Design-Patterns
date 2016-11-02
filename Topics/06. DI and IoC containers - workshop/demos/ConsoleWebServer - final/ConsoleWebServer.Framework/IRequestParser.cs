namespace ConsoleWebServer.Framework
{
    public interface IRequestParser
    {
        IHttpRequest Parse(string requestAsString);
    }
}