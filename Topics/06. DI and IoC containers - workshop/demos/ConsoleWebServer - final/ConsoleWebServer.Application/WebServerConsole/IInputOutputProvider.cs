namespace ConsoleWebServer.Application.WebServerConsole
{
    public interface IInputOutputProvider
    {
        string ReadInput();

        void WriteOutput(string value);
    }
}