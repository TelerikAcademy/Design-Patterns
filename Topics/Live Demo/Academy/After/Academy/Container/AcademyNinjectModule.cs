using Academy.Commands.Adding;
using Academy.Commands.Contracts;
using Academy.Commands.Creating;
using Academy.Commands.Listing;
using Academy.Core;
using Academy.Core.Contracts;
using Academy.Core.Factories;
using Academy.Core.Providers;
using Academy.Framework.Core.Contracts;
using Academy.Interceptors;
using Ninject;
using Ninject.Extensions.Interception.Infrastructure.Language;
using Ninject.Modules;

namespace Academy.Container
{
    public class AcademyNinjectModule : NinjectModule
    {
        private const string AddStudentToCourseName = "AddStudentToCourse";
        private const string AddStudentToSeasonName = "AddStudentToSeason";
        private const string AddTrainerToSeasonName = "AddTrainerToSeason";
        private const string CreateCourseName = "CreateCourse";
        private const string CreateCourseResultName = "CreateCourseResult";
        private const string CreateLectureName = "CreateLecture";
        private const string CreateSeasonName = "CreateSeason";
        private const string CreateStudentName = "CreateStudent";
        private const string CreateTrainerName = "CreateTrainer";
        private const string ListCoursesInSeasonName = "ListCoursesInSeason";
        private const string ListUsersName = "ListUsers";
        private const string ListUsersInSeasonName = "ListUsersInSeason";

        public override void Load()
        {
            this.Bind<IAcademyFactory>().To<AcademyFactory>().InSingletonScope();
            this.Bind<IAcademyDatabase>().To<AcademyDatabase>().InSingletonScope();
            this.Bind<ICommandFactory>().To<CommandFactory>().InSingletonScope().Intercept().With<StopwatchInterceptor>();

            this.Bind<IConfigurationProvider>().To<ConfigurationProvider>();
            this.Bind<ILogger>().To<ConsoleLogger>();
            this.Bind<IReader>().To<ConsoleReader>();
            this.Bind<IWriter>().To<ConsoleWriter>();
            this.Bind<IAuthProvider>().To<AuthProvider>();
            this.Bind<IParser>().To<CommandParser>().Intercept().With<StopwatchInterceptor>();
            this.Bind<IServiceLocator>().To<ServiceLocator>().Intercept().With<StopwatchInterceptor>();

            this.Bind<ICommand>().To<AddStudentToCourseCommand>().Named(AddStudentToCourseName).Intercept().With<StopwatchInterceptor>();
            this.Bind<ICommand>().To<AddStudentToSeasonCommand>().Named(AddStudentToSeasonName).Intercept().With<StopwatchInterceptor>();
            this.Bind<ICommand>().To<AddTrainerToSeasonCommand>().Named(AddTrainerToSeasonName).Intercept().With<StopwatchInterceptor>();
            this.Bind<ICommand>().To<CreateCourseCommand>().Named(CreateCourseName).Intercept().With<StopwatchInterceptor>();
            this.Bind<ICommand>().To<CreateCourseResultCommand>().Named(CreateCourseResultName).Intercept().With<StopwatchInterceptor>();
            this.Bind<ICommand>().To<CreateLectureCommand>().Named(CreateLectureName).Intercept().With<StopwatchInterceptor>();
            this.Bind<ICommand>().To<CreateSeasonCommand>().Named(CreateSeasonName).Intercept().With<StopwatchInterceptor>();
            this.Bind<ICommand>().To<CreateStudentCommand>().Named(CreateStudentName).Intercept().With<StopwatchInterceptor>();
            this.Bind<ICommand>().To<CreateTrainerCommand>().Named(CreateTrainerName).Intercept().With<StopwatchInterceptor>();
            this.Bind<ICommand>().To<ListCoursesInSeasonCommand>().Named(ListCoursesInSeasonName).Intercept().With<StopwatchInterceptor>();
            this.Bind<ICommand>().To<ListUsersCommand>().Named(ListUsersName).Intercept().With<StopwatchInterceptor>();
            this.Bind<ICommand>().To<ListUsersInSeasonCommand>().Named(ListUsersInSeasonName).Intercept().With<StopwatchInterceptor>();

            var engineBinding = this.Bind<IEngine>().To<Engine>().InSingletonScope();
            
            IConfigurationProvider configurationProvider = this.Kernel.Get<IConfigurationProvider>();
            if (!configurationProvider.IsTestEnvironment)
            {
                engineBinding.Intercept().With<AuthInterceptor>();
            }

            engineBinding.Intercept().With<StopwatchInterceptor>();
        }
    }
}