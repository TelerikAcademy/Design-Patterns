using System.Net;

namespace ConsoleWebServer.Framework
{
    public interface IHttResponseFactory
    {
        IHttpResponse CreateHttpResponse(string httpVersion, HttpStatusCode statusCode, string body, string contentType = HttpResponse.DefaultContentType);
    }
}