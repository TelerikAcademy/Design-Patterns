namespace ConsoleWebServer.Framework
{
    using System;
    using System.Net;

    using ConsoleWebServer.Framework.Handlers;

    public class ResponseProvider : IResponseProvider
    {
        private readonly Handler startHandler;

        public ResponseProvider(IHandlerFactory handlerFactory)
        {
            this.startHandler = handlerFactory.CreateAndAttachHandlers();
        }

        public HttpResponse GetResponse(string requestAsString)
        {
            IHttpRequest request;
            try
            {
                var requestParser = new RequestParser();
                request = requestParser.Parse(requestAsString);
            }
            catch (Exception ex)
            {
                return new HttpResponse(new Version(1, 1), HttpStatusCode.BadRequest, ex.Message);
            }

            var response = this.startHandler.HandleRequest(request);
            return response;
        }
    }
}
