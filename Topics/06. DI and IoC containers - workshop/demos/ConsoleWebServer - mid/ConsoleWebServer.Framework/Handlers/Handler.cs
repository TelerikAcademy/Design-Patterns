namespace ConsoleWebServer.Framework.Handlers
{
    using System.Net;

    public abstract class Handler
    {
        private Handler Successor { get; set; }

        public void SetSuccessor(Handler successor)
        {
            this.Successor = successor;
        }

        public HttpResponse HandleRequest(IHttpRequest request)
        {
            if (this.CanHandle(request))
            {
                return this.Handle(request);
            }
            
            if (this.Successor != null)
            {
                return this.Successor.HandleRequest(request);
            }

            return new HttpResponse(request.ProtocolVersion, HttpStatusCode.NotImplemented, "Request cannot be handled.");
        }

        protected abstract bool CanHandle(IHttpRequest request);

        protected abstract HttpResponse Handle(IHttpRequest request);
    }
}
