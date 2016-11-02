using System;
using System.IO;
using System.Net;

namespace ConsoleWebServer.Framework.Handlers
{
    public class StaticFileHandler : Handler
    {
        public StaticFileHandler(IHttResponseFactory httpResponseFactory)
            : base(httpResponseFactory)
        {
        }

        protected override bool CanHandle(IHttpRequest request)
        {
            return request.Uri.LastIndexOf(".", StringComparison.Ordinal)
                   > request.Uri.LastIndexOf("/", StringComparison.Ordinal);
        }

        protected override IHttpResponse Handle(IHttpRequest request)
        {
            var filePath = Environment.CurrentDirectory + "/" + request.Uri;
            if (!File.Exists(filePath))
            {
                return this.HttpResponseFactory.CreateHttpResponse(request.ProtocolVersion.ToString(), HttpStatusCode.NotFound, "File not found!");
            }

            var fileContents = File.ReadAllText(filePath);
            var response = this.HttpResponseFactory.CreateHttpResponse(request.ProtocolVersion.ToString(), HttpStatusCode.OK, fileContents);
            return response;
        }
    }
}