namespace ConsoleWebServer.Framework.Handlers
{
    using System.Linq;
    using System.Reflection;

    using ConsoleWebServer.Framework.ActionResults;

    public class ActionInvoker
    {
        public IActionResult InvokeAction(Controller controller, ActionDescriptor actionDescriptor)
        {
            var methodWithIntParameter =
                controller.GetType()
                    .GetMethods()
                    .FirstOrDefault(
                        x =>
                        x.Name.ToLower() == actionDescriptor.ActionName.ToLower() && x.GetParameters().Length == 1
                        && x.GetParameters()[0].ParameterType == typeof(string)
                        && x.ReturnType == typeof(IActionResult));
            if (methodWithIntParameter == null)
            {
                throw new HttpNotFoundException(
                    string.Format(
                        "Expected method with signature IActionResult {0}(string) in class {1}",
                        actionDescriptor.ActionName,
                        actionDescriptor.ControllerName));
            }

            try
            {
                var actionResult =
                    (IActionResult)methodWithIntParameter.Invoke(controller, new object[] { actionDescriptor.Parameter });
                return actionResult;
            }
            catch (TargetInvocationException ex)
            {
                throw ex.InnerException;
            }
        }
    }
}
