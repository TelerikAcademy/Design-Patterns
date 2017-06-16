namespace ProjectManager.Framework.Core.Commands.Contracts
{
    public interface ICommandsFactory
    {
        ICommand GetCommandFromString(string commandName);
    }
}
