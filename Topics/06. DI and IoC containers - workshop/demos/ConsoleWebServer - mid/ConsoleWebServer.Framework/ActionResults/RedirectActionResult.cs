namespace ConsoleWebServer.Framework.ActionResults
{
    using System.Collections.Generic;
    using System.Net;

    public class RedirectActionResult : BaseActionResult
    {
        public RedirectActionResult(IHttpRequest request, string location)
            : base(request)
        {
            this.ResponseHeaders.Add(new KeyValuePair<string, string>("Location", location));
        }

        protected override HttpStatusCode GetStatusCode()
        {
            return HttpStatusCode.Redirect;
        }
    }
}
