namespace ConsoleWebServer.Framework.ActionResults
{
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.Net;

    public class JsonActionResult : IActionResult
    {
        public readonly object model;

        public JsonActionResult(IHttpRequest request, object model)
        {
            this.model = model;
            this.Request = request;
            this.ResponseHeaders = new List<KeyValuePair<string, string>>();
        }

        public IHttpRequest Request { get; private set; }

        public List<KeyValuePair<string, string>> ResponseHeaders { get; private set; }

        public string GetContent()
        {
            return JsonConvert.SerializeObject(model);
        }

        public HttpResponse GetResponse()
        {
            var response = new HttpResponse(Request.ProtocolVersion, GetStatusCode(), GetContent(), "application/json");
            foreach (var responseHeader in ResponseHeaders)
            {
                response.AddHeader(responseHeader.Key, responseHeader.Value);
            }
            return response;
        }

        public virtual HttpStatusCode GetStatusCode()
        {
            return HttpStatusCode.OK;
        }
    }

    public class JsonActionResultWithCors : JsonActionResult
    {
        public JsonActionResultWithCors(IHttpRequest request, object model, string corsSettings)
            : base(request, model)
        {
            this.ResponseHeaders.Add(new KeyValuePair<string, string>("Access-Control-Allow-Origin", corsSettings));
        }
    }

    public class JsonActionResultWithoutCaching : JsonActionResult
    {
        public JsonActionResultWithoutCaching(IHttpRequest r, object model)
            : base(r, model)
        {
            this.ResponseHeaders.Add(new KeyValuePair<string, string>("Cache-Control", "private, max-age=0, no-cache"));
        }
    }

    public class JsonActionResultWithCorsWithoutCaching : JsonActionResult
    {
        public JsonActionResultWithCorsWithoutCaching(IHttpRequest request, object model, string corsSettings)
            : base(request, model)
        {
            this.ResponseHeaders.Add(new KeyValuePair<string, string>("Access-Control-Allow-Origin", corsSettings));
            this.ResponseHeaders.Add(new KeyValuePair<string, string>("Cache-Control", "private, max-age=0, no-cache"));
        }
    }
}
