using System;
using System.Collections.Generic;
using System.Net;

namespace ConsoleWebServer.Framework.ActionResults
{
    public class RedirectActionResult : BaseActionResult
    {
        public RedirectActionResult(IHttpRequest request, IHttResponseFactory httpResponseFactory, string location)
            : base(request, httpResponseFactory)
        {
            this.ResponseHeaders.Add(new KeyValuePair<string, string>("Location", location));
        }

        protected override HttpStatusCode GetStatusCode()
        {
            return HttpStatusCode.Redirect;
        }
    }
}