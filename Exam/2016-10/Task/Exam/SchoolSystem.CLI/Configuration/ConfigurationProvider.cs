using System.Configuration;

namespace SchoolSystem.Cli.Configuration
{
    public class ConfigurationProvider : IConfigurationProvider
    {
        public bool IsTestEnvironment
        {
            get
            {
                return bool.Parse(ConfigurationManager.AppSettings["IsTestEnvironment"]);
            }
        }
    }
}