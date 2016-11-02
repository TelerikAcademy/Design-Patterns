using System;
using System.Collections.Generic;
using System.Net;

namespace ConsoleWebServer.Framework.ActionResults
{
    public abstract class BaseActionResult : IActionResult
    {
        private readonly IHttpRequest request;
        private readonly IHttResponseFactory httpResponseFactory;
        private readonly IList<KeyValuePair<string, string>> responseHeaders;

        protected BaseActionResult(IHttpRequest request, IHttResponseFactory httpResponseFactory)
        {
            this.request = request;
            this.httpResponseFactory = httpResponseFactory;
            this.responseHeaders = new List<KeyValuePair<string, string>>();
        }

        protected IHttResponseFactory HttpResponseFactory
        {
            get
            {
                return this.httpResponseFactory;
            }
        }

        protected IList<KeyValuePair<string, string>> ResponseHeaders
        {
            get
            {
                return this.responseHeaders;
            }
        }

        public IHttpResponse GetResponse()
        {
            var response = this.HttpResponseFactory.CreateHttpResponse(this.request.ProtocolVersion.ToString(), this.GetStatusCode(), this.GetContent(), this.GetContentType());
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