namespace SchoolSystem.Cli.Configuration
{
    public interface IConfigurationProvider
    {
        bool IsTestEnvironment { get; }
    }
}