namespace ConsoleWebServer.Framework.Handlers
{
    using System;
    using System.IO;
    using System.Net;

    public class StaticFileHandler : Handler
    {
        protected override bool CanHandle(IHttpRequest request)
        {
            return request.Uri.LastIndexOf(".", StringComparison.Ordinal)
                   > request.Uri.LastIndexOf("/", StringComparison.Ordinal);
        }

        protected override HttpResponse Handle(IHttpRequest request)
        {
            var filePath = Environment.CurrentDirectory + "/" + request.Uri;
            if (!File.Exists(filePath))
            {
                return new HttpResponse(request.ProtocolVersion, HttpStatusCode.NotFound, "File not found!");
            }

            var fileContents = File.ReadAllText(filePath);
            var response = new HttpResponse(request.ProtocolVersion, HttpStatusCode.OK, fileContents);
            return response;
        }
    }
}
