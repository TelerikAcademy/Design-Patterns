using ConsoleWebServer.Application.Interceptors;
using ConsoleWebServer.Application.WebServerConsole;
using ConsoleWebServer.Framework;
using ConsoleWebServer.Framework.ActionResults;
using ConsoleWebServer.Framework.Handlers;
using Ninject;
using Ninject.Extensions.Conventions;
using Ninject.Extensions.Factory;
using Ninject.Extensions.Interception.Infrastructure.Language;
using Ninject.Modules;
using Ninject.Parameters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace ConsoleWebServer.Application
{
    public class WebServerModule : NinjectModule
    {
        private const string HeadHandlerName = "HeadHandler";
        private const string OptionsHandlerName = "OptionsHandler";
        private const string FileHandlerName = "FileHandler";
        private const string ControllerHandlerName = "ControllerHandler";

        private const string ContentActionResultName = "ContentActionResult";
        private const string JsonActionResultName = "JsonActionResult";
        private const string RedirectActionResultName = "RedirectActionResult";
        private const string ActionResultWithCorsName = "ActionResultWithCors";
        private const string ContentActionResultWithNoCachingName = "ContentActionResultWithNoCaching";

        private const string ActionResultConstructorArgument = "actionResult";
        private const string RequestConstructorArgument = "request";
        private const string ActionResultFactoryConstructorArgument = "actionResultFactory";

        public override void Load()
        {
            Kernel.Bind(x =>
            {
                x.FromAssembliesInPath(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location))
                .SelectAllClasses()
                .BindDefaultInterface();
            });

            Bind<IWebServerConsole>().To<WebServerConsole.ConsoleWebServer>().InSingletonScope();

            Bind<IHandler>().To<HeadHandler>().Named(HeadHandlerName);
            Bind<IHandler>().To<OptionsHandler>().Named(OptionsHandlerName);
            Bind<IHandler>().To<StaticFileHandler>().Named(FileHandlerName);
            Bind<IHandler>().To<ControllerHandler>().Named(ControllerHandlerName);
            Bind<IHandler>().ToMethod(context =>
            {
                IHandler headHandler = context.Kernel.Get<IHandler>(HeadHandlerName);
                IHandler optionsHandler = context.Kernel.Get<IHandler>(OptionsHandlerName);
                IHandler fileHandler = context.Kernel.Get<IHandler>(FileHandlerName);
                IHandler controllerHandler = context.Kernel.Get<IHandler>(ControllerHandlerName);

                headHandler.SetSuccessor(optionsHandler);
                optionsHandler.SetSuccessor(fileHandler);
                fileHandler.SetSuccessor(controllerHandler);

                return headHandler;
            }).WhenInjectedInto<ResponseProvider>();

            Bind<IHttResponseFactory>().ToFactory().InSingletonScope();
            Bind<IHttpRequestFactory>().ToFactory().InSingletonScope();
            Bind<IActionResultFactory>().ToFactory().InSingletonScope();
            Bind<IActionDescriptorFactory>().ToFactory().InSingletonScope();

            Bind<IActionResult>().To<ContentActionResult>().Named(ContentActionResultName);
            Bind<IActionResult>().To<JsonActionResult>().Named(JsonActionResultName);
            Bind<IActionResult>().To<RedirectActionResult>().Named(RedirectActionResultName);
            Bind<IActionResult>().To<ActionResultWithCorsDecorator>().Named(ActionResultWithCorsName);

            Bind<IActionResult>().To<ContentActionResult>().Named(ContentActionResultWithNoCachingName).Intercept().With<ActionResultWithNoCachingInterceptor>();

            Bind<IActionResult>()
                .ToMethod(context =>
                {
                    List<IParameter> contextParams = context.Parameters.ToList();
                    return context.Kernel.Get<IActionResult>(ActionResultWithCorsName, contextParams[2],
                        new ConstructorArgument(ActionResultConstructorArgument, context.Kernel.Get<IActionResult>(JsonActionResultName, contextParams[0], contextParams[1])));
                }).NamedLikeFactoryMethod((IActionResultFactory actionResultFactory) => actionResultFactory.GetJsonActionResultWithCors(null, null, null));

            Bind<IActionResult>()
                .ToMethod(context =>
                {
                    List<IParameter> contextParams = context.Parameters.ToList();
                    return context.Kernel.Get<IActionResult>(ActionResultWithCorsName, contextParams[2],
                        new ConstructorArgument(ActionResultConstructorArgument, context.Kernel.Get<IActionResult>(ContentActionResultWithNoCachingName, contextParams[0], contextParams[1])));
                }).NamedLikeFactoryMethod((IActionResultFactory actionResultFactory) => actionResultFactory.GetContentActionResultWithCorsAndNoCaching(null, null, null));

            Bind<Func<IHttpRequest, Controller>>()
                .ToMethod(context =>
                (request) =>
                {
                    string controllerClassName = request.Action.ControllerName + "Controller";
                    Type type =
                        Assembly.GetEntryAssembly()
                            .GetTypes()
                            .FirstOrDefault(
                                x => x.Name.ToLower() == controllerClassName.ToLower() && typeof(Controller).IsAssignableFrom(x));

                    if (type == null)
                    {
                        throw new HttpNotFoundException(
                            string.Format("Controller with name {0} not found!", controllerClassName));
                    }

                    Controller instance = (Controller)context.Kernel.Get(type, 
                        new ConstructorArgument(RequestConstructorArgument, request), 
                        new ConstructorArgument(ActionResultFactoryConstructorArgument, context.Kernel.Get<IActionResultFactory>()));
                    return instance;
                }).InSingletonScope();
        }
    }
}