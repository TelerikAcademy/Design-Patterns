namespace ConsoleWebServer.Framework.ActionResults
{
    public class ActionResultWithCorsDecorator : IActionResult
    {
        private readonly string corsSettings;
        private readonly IActionResult actionResult;

        public ActionResultWithCorsDecorator(string corsSettings, IActionResult actionResult)
        {
            this.corsSettings = corsSettings;
            this.actionResult = actionResult;
        }

        public IHttpResponse GetResponse()
        {
            IHttpResponse response = this.actionResult.GetResponse();
            if (response != null)
            {
                response.AddHeader("Access-Control-Allow-Origin", this.corsSettings);
            }

            return response;
        }
    }
}