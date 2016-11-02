using System;
using System.Net;

namespace ConsoleWebServer.Framework.ActionResults
{
    public class ContentActionResult : BaseActionResult
    {
        private readonly object model;

        public ContentActionResult(IHttpRequest request, IHttResponseFactory httpResponseFactory, object model)
            : base(request, httpResponseFactory)
        {
            this.model = model;
        }

        protected override string GetContent()
        {
            return this.model.ToString();
        }
    }
}