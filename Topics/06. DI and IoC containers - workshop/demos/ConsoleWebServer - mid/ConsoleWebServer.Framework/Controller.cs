namespace ConsoleWebServer.Framework
{
    using ConsoleWebServer.Framework.ActionResults;

    public abstract class Controller
    {
        protected Controller(IHttpRequest request)
        {
            this.Request = request;
        }

        protected IHttpRequest Request { get; private set; }

        protected IActionResult Content(object model)
        {
            return new ContentActionResult(this.Request, model);
        }

        protected IActionResult Json(object model)
        {
            return new JsonActionResult(this.Request, model);
        }

        protected IActionResult Redirect(string location)
        {
            return new RedirectActionResult(this.Request, location);
        }
    }
}
