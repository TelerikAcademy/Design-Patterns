using ConsoleWebServer.Framework;
using ConsoleWebServer.Framework.ActionResults;

namespace ConsoleWebServer.Application.Controllers
{
    public class HomeController : Controller
    {
        public HomeController(IHttpRequest request, IActionResultFactory actionResultFactory)
            : base(request, actionResultFactory)
        {
        }

        public IActionResult Index(string param)
        {
            return this.Content("Home page :)");
        }

        public IActionResult LivePage(string param)
        {
            return this.ActionResultFactory.GetContentActionResultWithNoCaching(this.Request, "Live page with no caching");
        }

        public IActionResult LivePageForAjax(string param)
        {
            return this.ActionResultFactory.GetContentActionResultWithCorsAndNoCaching(this.Request, "Live page with no caching and CORS", "*");
        }

        public IActionResult Forum(string param)
        {
            return this.Redirect("https://telerikacademy.com/Forum/Home");
        }
    }
}