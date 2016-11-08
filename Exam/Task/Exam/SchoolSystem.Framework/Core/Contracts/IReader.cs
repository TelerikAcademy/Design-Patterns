namespace SchoolSystem.Framework.Core.Contracts
{
    /// <summary>
    /// Interface for a source Reader provider
    /// </summary>
    public interface IReader
    {
        /// <summary>
        /// Reads data from a specific source.
        /// </summary>
        /// <returns>Returns the read data as string.</returns>
        string ReadLine();
    }
}
