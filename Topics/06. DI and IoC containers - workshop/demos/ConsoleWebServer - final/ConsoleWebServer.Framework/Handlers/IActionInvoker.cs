using ConsoleWebServer.Framework.ActionResults;

namespace ConsoleWebServer.Framework.Handlers
{
    public interface IActionInvoker
    {
        IActionResult InvokeAction(Controller controller, IActionDescriptor actionDescriptor);
    }
}