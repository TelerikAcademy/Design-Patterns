namespace ConsoleWebServer.Framework.ActionResults
{
    using Newtonsoft.Json;

    public class JsonActionResult : BaseActionResult
    {
        private readonly object model;

        public JsonActionResult(IHttpRequest request, object model)
            : base(request)
        {
            this.model = model;
        }

        protected override string GetContent()
        {
            return JsonConvert.SerializeObject(this.model);
        }

        protected override string GetContentType()
        {
            return "application/json";
        }
    }
}
