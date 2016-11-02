using System;
using System.Net;
using ConsoleWebServer.Framework.Handlers;

namespace ConsoleWebServer.Framework
{
    public class ResponseProvider : IResponseProvider
    {
        private readonly IHandler startHandler;
        private readonly IRequestParser requestParser;
        private readonly IHttResponseFactory httpResponseFactory;

        public ResponseProvider(IHandler startHandler, IRequestParser requestParser, IHttResponseFactory httpResponseFactory)
        {
            this.startHandler = startHandler;
            this.requestParser = requestParser;
            this.httpResponseFactory = httpResponseFactory;
        }

        public IHttpResponse GetResponse(string requestAsString)
        {
            IHttpRequest request;
            try
            {
                request = this.requestParser.Parse(requestAsString);
            }
            catch (Exception ex)
            {
                return this.httpResponseFactory.CreateHttpResponse(new Version(1, 1).ToString(), HttpStatusCode.BadRequest, ex.Message);
            }

            IHttpResponse response = this.startHandler.HandleRequest(request);
            return response;
        }
    }
}