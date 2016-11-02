using ConsoleWebServer.Framework.ActionResults;

namespace ConsoleWebServer.Framework
{
    public interface IActionResultFactory
    {
        IActionResult GetContentActionResult(IHttpRequest request, object model);

        IActionResult GetJsonActionResult(IHttpRequest request, object model);

        IActionResult GetRedirectActionResult(IHttpRequest request, string location);

        IActionResult GetContentActionResultWithNoCaching(IHttpRequest request, object model);

        IActionResult GetJsonActionResultWithCors(IHttpRequest request, object model, string corsSettings);

        IActionResult GetContentActionResultWithCorsAndNoCaching(IHttpRequest request, object model, string corsSettings);
    }
}