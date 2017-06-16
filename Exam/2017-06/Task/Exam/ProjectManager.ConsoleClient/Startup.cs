using ProjectManager.ConsoleClient.Configs;
using ProjectManager.Framework.Core;
using ProjectManager.Framework.Core.Commands.Factories;
using ProjectManager.Framework.Core.Common.Providers;
using ProjectManager.Framework.Data;
using ProjectManager.Framework.Data.Factories;
using ProjectManager.Framework.Services;

namespace ProjectManager.ConsoleClient
{
    public class Startup
    {
        public static void Main()
        {
            var configProvider = new ConfigurationProvider();

            // This is an example of how to create the caching service. Think about how and where to use it in the project.
            var cacheService = new CachingService(configProvider.CacheDurationInSeconds);

            var fileLogger = new FileLogger(configProvider.LogFilePath);

            var engine = new Engine(fileLogger);

            engine.Start();
        }
    }
}
