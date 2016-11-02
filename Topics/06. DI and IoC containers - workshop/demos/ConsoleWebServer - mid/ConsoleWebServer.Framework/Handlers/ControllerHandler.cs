namespace ConsoleWebServer.Framework.Handlers
{
    using System;
    using System.Net;

    public class ControllerHandler : Handler
    {
        protected override bool CanHandle(IHttpRequest request)
        {
            return request.ProtocolVersion.Major < 3;
        }

        protected override HttpResponse Handle(IHttpRequest request)
        {
            HttpResponse response;
            try
            {
                IControllerFactory controllerFactory = new ControllerFactory();
                var controller = controllerFactory.CreateController(request);
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
    }
}
