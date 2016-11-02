using System;
using System.Linq;
using System.Net;
using System.Reflection;
using ConsoleWebServer.Framework.ActionResults;

namespace ConsoleWebServer.Framework.Handlers
{
    public class OptionsHandler : Handler
    {
        public OptionsHandler(IHttResponseFactory httpResponseFactory)
            : base(httpResponseFactory)
        {
        }

        protected override bool CanHandle(IHttpRequest request)
        {
            return request.Method.ToLower() == "options";
        }

        protected override IHttpResponse Handle(IHttpRequest request)
        {
            var routes =
                Assembly.GetEntryAssembly()
                    .GetTypes()
                    .Where(x => x.Name.EndsWith("Controller") && typeof(Controller).IsAssignableFrom(x))
                    .Select(
                        x => new { x.Name, Methods = x.GetMethods().Where(m => m.ReturnType == typeof(IActionResult)) })
                    .SelectMany(
                        x =>
                        x.Methods.Select(
                            m =>
                            string.Format("/{0}/{1}/{{parameter}}", x.Name.Replace("Controller", string.Empty), m.Name)))
                    .ToList();

            return this.HttpResponseFactory.CreateHttpResponse(request.ProtocolVersion.ToString(), HttpStatusCode.OK, string.Join(Environment.NewLine, routes));
        }
    }
}