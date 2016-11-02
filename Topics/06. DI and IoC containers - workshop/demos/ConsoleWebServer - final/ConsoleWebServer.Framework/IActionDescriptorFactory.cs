namespace ConsoleWebServer.Framework
{
    public interface IActionDescriptorFactory
    {
        IActionDescriptor CreateActionDescriptor(string uri);
    }
}