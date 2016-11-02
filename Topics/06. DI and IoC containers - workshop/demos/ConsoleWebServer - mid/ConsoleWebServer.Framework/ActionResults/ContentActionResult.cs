namespace ConsoleWebServer.Framework.ActionResults
{
    public class ContentActionResult : BaseActionResult
    {
        private readonly object model;

        public ContentActionResult(IHttpRequest request, object model)
            : base(request)
        {
            this.model = model;
        }

        protected override string GetContent()
        {
            return this.model.ToString();
        }
    }
}
