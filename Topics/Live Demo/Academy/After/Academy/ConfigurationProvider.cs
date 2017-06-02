using Academy.Core.Contracts;
using System.Configuration;

namespace Academy.Core.Providers
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