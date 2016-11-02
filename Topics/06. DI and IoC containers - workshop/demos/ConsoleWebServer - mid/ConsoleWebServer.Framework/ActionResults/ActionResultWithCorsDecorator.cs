namespace ConsoleWebServer.Framework.ActionResults
{
    public class ActionResultWithCorsDecorator : ActionResultDecorator
    {
        private readonly string corsSettings;

        public ActionResultWithCorsDecorator(string corsSettings, IActionResult actionResult)
            : base(actionResult)
        {
            this.corsSettings = corsSettings;
        }

        protected override void UpdateResponse(HttpResponse response)
        {
            response.AddHeader("Access-Control-Allow-Origin", this.corsSettings);
        }
    }
}
