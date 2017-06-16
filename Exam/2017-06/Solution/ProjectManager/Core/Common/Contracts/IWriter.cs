namespace ProjectManager.Framework.Core.Common.Contracts
{
    public interface IWriter
    {
        void Write(object value);

        void WriteLine(object value);
    }
}
