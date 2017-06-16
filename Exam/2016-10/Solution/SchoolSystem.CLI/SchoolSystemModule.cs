using Ninject;
using Ninject.Extensions.Conventions;
using Ninject.Extensions.Factory;
using Ninject.Extensions.Interception.Infrastructure.Language;
using Ninject.Modules;
using SchoolSystem.Cli.Configuration;
using SchoolSystem.Cli.Interceptors;
using SchoolSystem.Framework.Core;
using SchoolSystem.Framework.Core.Commands;
using SchoolSystem.Framework.Core.Commands.Contracts;
using SchoolSystem.Framework.Core.Contracts;
using SchoolSystem.Framework.Core.Providers;
using SchoolSystem.Framework.Models;
using SchoolSystem.Framework.Models.Contracts;
using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace SchoolSystem.Cli
{
    public class SchoolSystemModule : NinjectModule
    {
        public override void Load()
        {
            Kernel.Bind(x =>
            {
                x.FromAssembliesInPath(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location))
                .SelectAllClasses()
                .Where(type => type != typeof(Engine))
                .BindDefaultInterface();
            });

            Bind<IEngine>().To<Engine>().InSingletonScope();
            
            Bind<CreateStudentCommand>().ToSelf().InSingletonScope();
            Bind<CreateTeacherCommand>().ToSelf().InSingletonScope();
            Bind<RemoveStudentCommand>().ToSelf().InSingletonScope();
            Bind<RemoveTeacherCommand>().ToSelf().InSingletonScope();
            Bind<StudentListMarksCommand>().ToSelf().InSingletonScope();
            Bind<TeacherAddMarkCommand>().ToSelf().InSingletonScope();

            Bind<IReader>().To<ConsoleReaderProvider>().InSingletonScope();
            Bind<IWriter>().To<ConsoleWriterProvider>().InSingletonScope();
            Bind<IParser>().To<CommandParserProvider>().InSingletonScope();

            var commandFactoryBinding = Bind<ICommandFactory>().ToFactory().InSingletonScope();
            var studentFactoryBinding = Bind<IStudentFactory>().ToFactory().InSingletonScope();
            var markFactoryBinding = Bind<IMarkFactory>().ToFactory().InSingletonScope();

            IConfigurationProvider configurationProvider = Kernel.Get<IConfigurationProvider>();
            if (configurationProvider.IsTestEnvironment)
            {
                commandFactoryBinding.Intercept().With<StopwatchInterceptor>();
                studentFactoryBinding.Intercept().With<StopwatchInterceptor>();
                markFactoryBinding.Intercept().With<StopwatchInterceptor>();
            }

            Bind<ITeacherFactory>().ToFactory().InSingletonScope();

            Bind(typeof(IAddStudent), typeof(IAddTeacher), typeof(IRemoveStudent), typeof(IRemoveTeacher), typeof(IGetStudent), typeof(IGetTeacher), typeof(IGetStudentAndTeacher))
                .To<School>()
                .InSingletonScope();

            Bind<ICommand>().ToMethod(context =>
            {
                Type commandType = (Type)context.Parameters.Single().GetValue(context, null);
                return (ICommand)context.Kernel.Get(commandType);
            }).NamedLikeFactoryMethod((ICommandFactory commandFactory) => commandFactory.GetCommand(null));
        }
    }
}