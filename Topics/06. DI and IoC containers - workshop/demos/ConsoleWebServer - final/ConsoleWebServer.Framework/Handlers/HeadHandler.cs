using System;
using System.Net;

namespace ConsoleWebServer.Framework.Handlers
{
    public class HeadHandler : Handler
    {
        public HeadHandler(IHttResponseFactory httpResponseFactory)
            : base(httpResponseFactory)
        {
        }

        protected override bool CanHandle(IHttpRequest request)
        {
            return request.Method.ToLower() == "head";
        }

        protected override IHttpResponse Handle(IHttpRequest request)
        {
            return this.HttpResponseFactory.CreateHttpResponse(request.ProtocolVersion.ToString(), HttpStatusCode.OK, string.Empty);
        }
    }
}