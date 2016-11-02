namespace ConsoleWebServer.Framework.ActionResults
{
    using System.Collections.Generic;
    using System.Net;

    public abstract class BaseActionResult : IActionResult
    {
        protected BaseActionResult(IHttpRequest request)
        {
            this.Request = request;
            this.ResponseHeaders = new List<KeyValuePair<string, string>>();
        }

        protected List<KeyValuePair<string, string>> ResponseHeaders { get; private set; }

        private IHttpRequest Request { get; set; }

        public HttpResponse GetResponse()
        {
            var response = new HttpResponse(this.Request.ProtocolVersion, this.GetStatusCode(), this.GetContent(), this.GetContentType());
            foreach (var responseHeader in this.ResponseHeaders)
            {
                response.AddHeader(responseHeader.Key, responseHeader.Value);
            }

            return response;
        }

        protected virtual HttpStatusCode GetStatusCode()
        {
            return HttpStatusCode.OK;
        }

        protected virtual string GetContent()
        {
            return string.Empty;
        }

        protected virtual string GetContentType()
        {
            return HttpResponse.DefaultContentType;
        }
    }
}
