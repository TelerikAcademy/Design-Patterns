namespace ConsoleWebServer.Framework
{
    public interface IActionDescriptor
    {
        string ControllerName { get; }
        string ActionName { get; }
        string Parameter { get; }
    }
}