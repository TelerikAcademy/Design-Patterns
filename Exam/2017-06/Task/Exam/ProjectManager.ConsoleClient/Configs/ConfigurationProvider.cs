using System;
using System.Configuration;

namespace ProjectManager.ConsoleClient.Configs
{
    public class ConfigurationProvider : IConfigurationProvider
    {
        public TimeSpan CacheDurationInSeconds
        {
            get
            {
                return TimeSpan.FromSeconds(int.Parse(ConfigurationManager.AppSettings["CacheTime"]));
            }
        }

        public string LogFilePath
        {
            get
            {
                return ConfigurationManager.AppSettings["LogFilePath"];
            }
        }
    }
}
