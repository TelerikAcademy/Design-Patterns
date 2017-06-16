namespace ProjectManager.Framework.Core.Common.Contracts
{
    public interface IProcessor
    {
        string ProcessCommand(string commandLine);
    }
}
