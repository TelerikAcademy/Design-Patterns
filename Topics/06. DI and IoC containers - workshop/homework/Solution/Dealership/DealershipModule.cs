using Dealership.CommandHandlers;
using Dealership.Engine;
using Dealership.Factories;
using Ninject;
using Ninject.Extensions.Conventions;
using Ninject.Extensions.Factory;
using Ninject.Modules;
using System.IO;
using System.Reflection;

namespace Dealership
{
    public class DealershipModule : NinjectModule
    {
        private const string UserNotLoggedCommandHandlerName = "UserNotLoggedCommandHandlerName";
        private const string ShowVehiclesCommandHandlerName = "ShowVehiclesCommandHandlerName";
        private const string ShowUsersCommandHandlerName = "ShowUsersCommandHandlerName";
        private const string RemoveVehicleCommandHandlerName = "RemoveVehicleCommandHandlerName";
        private const string RemoveCommentCommandHandlerName = "RemoveCommentCommandHandlerName";
        private const string RegisterUserCommandHandlerName = "RegisterUserCommandHandlerName";
        private const string LogoutCommandHandlerName = "LogoutCommandHandlerName";
        private const string LoginCommandHandlerName = "LoginCommandHandlerName";
        private const string AddVehicleCommandHandlerName = "AddVehicleCommandHandlerName";
        private const string AddCommentCommandHandlerName = "AddCommentCommandHandlerName";

        public override void Load()
        {
            Kernel.Bind(x =>
            {
                x.FromAssembliesInPath(Path.GetDirectoryName(Assembly.GetCallingAssembly().Location))
                .SelectAllClasses()
                .Where(type => type != typeof(UserProvider))
                .BindDefaultInterface();
            });

            Bind<IDealershipFactory>().ToFactory().InSingletonScope();
            Bind<IEngine>().To<DealershipEngine>().InSingletonScope();
            Bind<IUserProvider>().To<UserProvider>().InSingletonScope();
            Bind<IInputOutputProvider>().To<ConsoleInputOutputProvider>().InSingletonScope();

            Bind<ICommandHandler>().To<UserNotLoggedCommandHandler>().Named(UserNotLoggedCommandHandlerName);
            Bind<ICommandHandler>().To<ShowVehiclesCommandHandler>().Named(ShowVehiclesCommandHandlerName);
            Bind<ICommandHandler>().To<ShowUsersCommandHandler>().Named(ShowUsersCommandHandlerName);
            Bind<ICommandHandler>().To<RemoveVehicleCommandHandler>().Named(RemoveVehicleCommandHandlerName);
            Bind<ICommandHandler>().To<RemoveCommentCommandHandler>().Named(RemoveCommentCommandHandlerName);
            Bind<ICommandHandler>().To<RegisterUserCommandHandler>().Named(RegisterUserCommandHandlerName);
            Bind<ICommandHandler>().To<LogoutCommandHandler>().Named(LogoutCommandHandlerName);
            Bind<ICommandHandler>().To<LoginCommandHandler>().Named(LoginCommandHandlerName);
            Bind<ICommandHandler>().To<AddVehicleCommandHandler>().Named(AddVehicleCommandHandlerName);
            Bind<ICommandHandler>().To<AddCommentCommandHandler>().Named(AddCommentCommandHandlerName);

            Bind<ICommandHandlerProcessor>().ToMethod(context =>
            {
                ICommandHandler userNotLoggedHandler = context.Kernel.Get<ICommandHandler>(UserNotLoggedCommandHandlerName);
                ICommandHandler showVehiclesHandler = context.Kernel.Get<ICommandHandler>(ShowVehiclesCommandHandlerName);
                ICommandHandler showUsersHandler = context.Kernel.Get<ICommandHandler>(ShowUsersCommandHandlerName);
                ICommandHandler removeVehicleHandler = context.Kernel.Get<ICommandHandler>(RemoveVehicleCommandHandlerName);
                ICommandHandler removeCommentHandler = context.Kernel.Get<ICommandHandler>(RemoveCommentCommandHandlerName);
                ICommandHandler registerUserHandler = context.Kernel.Get<ICommandHandler>(RegisterUserCommandHandlerName);
                ICommandHandler logoutHandler = context.Kernel.Get<ICommandHandler>(LogoutCommandHandlerName);
                ICommandHandler loginHandler = context.Kernel.Get<ICommandHandler>(LoginCommandHandlerName);
                ICommandHandler addVehicleHandler = context.Kernel.Get<ICommandHandler>(AddVehicleCommandHandlerName);
                ICommandHandler addCommentHandler = context.Kernel.Get<ICommandHandler>(AddCommentCommandHandlerName);

                userNotLoggedHandler.SetSuccessor(showVehiclesHandler);
                showVehiclesHandler.SetSuccessor(showUsersHandler);
                showUsersHandler.SetSuccessor(removeVehicleHandler);
                removeVehicleHandler.SetSuccessor(removeCommentHandler);
                removeCommentHandler.SetSuccessor(registerUserHandler);
                registerUserHandler.SetSuccessor(logoutHandler);
                logoutHandler.SetSuccessor(loginHandler);
                loginHandler.SetSuccessor(addVehicleHandler);
                addVehicleHandler.SetSuccessor(addCommentHandler);

                return userNotLoggedHandler;
            }).WhenInjectedInto<DealershipEngine>();
        }
    }
}