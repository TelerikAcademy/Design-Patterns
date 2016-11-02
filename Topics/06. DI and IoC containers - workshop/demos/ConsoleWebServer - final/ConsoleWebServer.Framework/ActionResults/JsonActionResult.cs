using Newtonsoft.Json;
using System;
using System.Net;

namespace ConsoleWebServer.Framework.ActionResults
{
    public class JsonActionResult : BaseActionResult
    {
        private readonly object model;

        public JsonActionResult(IHttpRequest request, IHttResponseFactory httpResponseFactory, object model)
            : base(request, httpResponseFactory)
        {
            this.model = model;
        }

        protected override string GetContent()
        {
            return JsonConvert.SerializeObject(this.model);
        }

        protected override string GetContentType()
        {
            return "application/json";
        }
    }
}