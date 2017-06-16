using System;

namespace ProjectManager.ConsoleClient.Configs
{
    public interface IConfigurationProvider
    {
        TimeSpan CacheDurationInSeconds { get;}

        string LogFilePath { get; }
    }
}
