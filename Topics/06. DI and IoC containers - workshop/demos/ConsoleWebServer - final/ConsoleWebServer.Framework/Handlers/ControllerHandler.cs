using System;
using System.Net;

namespace ConsoleWebServer.Framework.Handlers
{
    public class ControllerHandler : Handler
    {
        private readonly Func<IHttpRequest, Controller> controllerFactory;

        private readonly IActionInvoker actionInvoker;

        public ControllerHandler(Func<IHttpRequest, Controller> controllerFactory, IActionInvoker actionInvoker, IHttResponseFactory httpResponseFactory)
            : base(httpResponseFactory)
        {
            this.controllerFactory = controllerFactory;
            this.actionInvoker = actionInvoker;
        }

        protected override bool CanHandle(IHttpRequest request)
        {
            return request.ProtocolVersion.Major < 3;
        }

        protected override IHttpResponse Handle(IHttpRequest request)
        {
            IHttpResponse response;
            try
            {
                var controller = this.controllerFactory(request);
                var actionResult = this.actionInvoker.InvokeAction(controller, request.Action);
                response = actionResult.GetResponse();
            }
            catch (HttpNotFoundException exception)
            {
                response = this.HttpResponseFactory.CreateHttpResponse(request.ProtocolVersion.ToString(), HttpStatusCode.NotFound, exception.Message);
            }
            catch (Exception exception)
            {
                response = this.HttpResponseFactory.CreateHttpResponse(request.ProtocolVersion.ToString(), HttpStatusCode.InternalServerError, exception.Message);
            }

            return response;
        }
    }
}