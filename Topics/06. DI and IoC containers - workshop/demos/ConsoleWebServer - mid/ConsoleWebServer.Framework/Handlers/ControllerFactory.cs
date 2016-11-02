namespace ConsoleWebServer.Framework.Handlers
{
    using System;
    using System.Linq;
    using System.Reflection;

    public class ControllerFactory : IControllerFactory
    {
        public Controller CreateController(IHttpRequest request)
        {
            var controllerClassName = request.Action.ControllerName + "Controller";
            var type =
                Assembly.GetEntryAssembly()
                    .GetTypes()
                    .FirstOrDefault(
                        x => x.Name.ToLower() == controllerClassName.ToLower() && typeof(Controller).IsAssignableFrom(x));
            
            if (type == null)
            {
                throw new HttpNotFoundException(
                    string.Format("Controller with name {0} not found!", controllerClassName));
            }

            var instance = (Controller)Activator.CreateInstance(type, request);
            return instance;
        }
    }
}
