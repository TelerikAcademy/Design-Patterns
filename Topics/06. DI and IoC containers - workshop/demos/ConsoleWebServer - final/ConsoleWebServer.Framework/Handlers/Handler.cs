using System.Net;

namespace ConsoleWebServer.Framework.Handlers
{
    public abstract class Handler : IHandler
    {
        private readonly IHttResponseFactory httpResponseFactory;

        public Handler(IHttResponseFactory httpResponseFactory)
        {
            this.httpResponseFactory = httpResponseFactory;
        }

        protected IHttResponseFactory HttpResponseFactory
        {
            get
            {
                return this.httpResponseFactory;
            }
        }

        private IHandler Successor { get; set; }

        public void SetSuccessor(IHandler successor)
        {
            this.Successor = successor;
        }

        public IHttpResponse HandleRequest(IHttpRequest request)
        {
            if (this.CanHandle(request))
            {
                return this.Handle(request);
            }
            
            if (this.Successor != null)
            {
                return this.Successor.HandleRequest(request);
            }

            return this.HttpResponseFactory.CreateHttpResponse(request.ProtocolVersion.ToString(), HttpStatusCode.NotImplemented, "Request cannot be handled.");
        }

        protected abstract bool CanHandle(IHttpRequest request);

        protected abstract IHttpResponse Handle(IHttpRequest request);
    }
}