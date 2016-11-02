namespace ConsoleWebServer.Framework
{
    public interface IHttpRequestFactory
    {
        IHttpRequest CreateHttpRequest(string method, string uri, string httpVersion);
    }
}