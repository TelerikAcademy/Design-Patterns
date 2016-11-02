namespace ConsoleWebServer.Framework
{
    using System;
    using System.Linq;
    using System.Net;

    using ConsoleWebServer.Framework.Handlers;
    using System.Reflection;
    using ActionResults;

    public class ResponseProvider
    {
        public ResponseProvider()
        {
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

            var response = this.Process(request);
            return response;
        }

        private HttpResponse Process(IHttpRequest request)
        {
            if (request.Method.ToLower() == "head")
            {
                return new HttpResponse(request.ProtocolVersion, HttpStatusCode.OK, string.Empty);
            }
            else if (request.Method.ToLower() == "options")
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

                return new HttpResponse(request.ProtocolVersion, HttpStatusCode.OK, string.Join(Environment.NewLine, routes));
            }
            else if (new StaticFileHandler().CanHandle(request))
            {
                return new StaticFileHandler().Handle(request);
            }
            else if (request.ProtocolVersion.Major < 3)
            {
                HttpResponse response;
                try
                {
                    var controller = CreateController(request);
                    var actionInvoker = new ActionInvoker();
                    var actionResult = actionInvoker.InvokeAction(controller, request.Action);
                    response = actionResult.GetResponse();
                }
                catch (HttpNotFoundException exception)
                {
                    response = new HttpResponse(request.ProtocolVersion, HttpStatusCode.NotFound, exception.Message);
                }
                catch (Exception exception)
                {
                    response = new HttpResponse(request.ProtocolVersion, HttpStatusCode.InternalServerError, exception.Message);
                }
                return response;
            }
            else
            {
                return new HttpResponse(request.ProtocolVersion, HttpStatusCode.NotImplemented, "Request cannot be handled.");
            }
        }

        private static Controller CreateController(IHttpRequest request)
        {
            var controllerClassName = request.Action.ControllerName + "Controller";
            var type =
                Assembly.GetEntryAssembly()
                    .GetTypes()
                    .FirstOrDefault(
                        x => x.Name.ToLower() == controllerClassName.ToLower() && typeof(Controller).IsAssignableFrom(x));
            if (type == null)
            {
                throw new HttpNotFoundException(
                    string.Format("Controller with name {0} not found!", controllerClassName));
            }
            var instance = (Controller)Activator.CreateInstance(type, request);
            return instance;
        }
    }
}