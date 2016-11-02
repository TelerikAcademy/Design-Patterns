namespace ConsoleWebServer.Framework
{
    /// <summary>
    /// Defines a method to provide response by given request as string.
    /// </summary>
    public interface IResponseProvider
    {
        /// <summary>
        /// Returns response object created by the given request.
        /// </summary>
        /// <param name="requestAsString">The request information given as string.</param>
        /// <returns>Returns the response object</returns>
        HttpResponse GetResponse(string requestAsString);
    }
}
