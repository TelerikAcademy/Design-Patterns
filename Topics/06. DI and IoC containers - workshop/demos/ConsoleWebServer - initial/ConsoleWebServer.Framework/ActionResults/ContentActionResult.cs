using System.Collections.Generic;
using System.Net;

namespace ConsoleWebServer.Framework.ActionResults
{
    public class ContentActionResult : IActionResult
    {
        public readonly object model;

        public ContentActionResult(IHttpRequest request, object model)
        {
            this.model = model;
            this.Request = request;
            this.ResponseHeaders = new List<KeyValuePair<string, string>>();
        }

        public IHttpRequest Request { get; private set; }

        public List<KeyValuePair<string, string>> ResponseHeaders { get; private set; }

        public HttpResponse GetResponse()
        {
            var response = new HttpResponse(this.Request.ProtocolVersion, HttpStatusCode.OK, this.model.ToString(), "text/plain; charset=utf-8");
            foreach (var responseHeader in this.ResponseHeaders) { response.AddHeader(responseHeader.Key, responseHeader.Value); }
            return response;
        }
    }

    public class ContentActionResultWithoutCaching : ContentActionResult
    {
        public ContentActionResultWithoutCaching(IHttpRequest request, object model) : base(request, model)
        {
            this.ResponseHeaders.Add(new KeyValuePair<string, string>("Cache-Control", "private, max-age=0, no-cache"));
        }
    }

    public class ContentActionResultWithCors : ContentActionResult
    {
        public ContentActionResultWithCors(IHttpRequest request, object model, string corsSettings) : base(request, model)
        {
            this.ResponseHeaders.Add(new KeyValuePair<string, string>("Access-Control-Allow-Origin", corsSettings));
        }
    }

    public class ContentActionResultWithCorsWithoutCaching : ContentActionResult
    {
        public ContentActionResultWithCorsWithoutCaching(IHttpRequest request, object model, string corsSettings) : base(request, model)
        {
            this.ResponseHeaders.Add(new KeyValuePair<string, string>("Access-Control-Allow-Origin", corsSettings));
            this.ResponseHeaders.Add(new KeyValuePair<string, string>("Cache-Control", "private, max-age=0, no-cache"));
        }
    }
}