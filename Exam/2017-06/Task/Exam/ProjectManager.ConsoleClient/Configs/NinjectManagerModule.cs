using Ninject.Modules;

namespace ProjectManager.Configs
{
    public class NinjectManagerModule : NinjectModule
    {
        // TODO: TODODODODODOD when I have more time
        public override void Load()
        {
            //Kernel.Bind(x =>
            //{
            //    x.FromAssembliesInPath(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location))
            //    .SelectAllClasses()
            //    .Where(type => type != typeof(Engine))
            //    .BindDefaultInterface();
            //});

            //IConfigurationProvider configurationProvider = Kernel.Get<IConfigurationProvider>();

            //this.Bind<ILogger>().To<FileLogger>().InSingletonScope().WithConstructorArgument(configurationProvider.LogFilePath);
        }
    }
}
