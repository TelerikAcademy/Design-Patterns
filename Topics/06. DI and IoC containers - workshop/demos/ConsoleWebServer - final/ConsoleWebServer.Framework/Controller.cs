using ConsoleWebServer.Framework.ActionResults;

namespace ConsoleWebServer.Framework
{
    public abstract class Controller
    {
        private readonly IActionResultFactory actionResultFactory;

        protected Controller(IHttpRequest request, IActionResultFactory actionResultFactory)
        {
            this.actionResultFactory = actionResultFactory;
            this.Request = request;
        }

        protected IActionResultFactory ActionResultFactory
        {
            get
            {
                return this.actionResultFactory;
            }
        }

        protected IHttpRequest Request { get; private set; }

        protected IActionResult Content(object model)
        {
            return this.ActionResultFactory.GetContentActionResult(this.Request, model);
        }

        protected IActionResult Json(object model)
        {
            return this.ActionResultFactory.GetJsonActionResult(this.Request, model);
        }

        protected IActionResult Redirect(string location)
        {
            return this.ActionResultFactory.GetRedirectActionResult(this.Request, location);
        }
    }
}