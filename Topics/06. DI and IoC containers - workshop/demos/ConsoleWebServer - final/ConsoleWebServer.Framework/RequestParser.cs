using System.IO;

namespace ConsoleWebServer.Framework
{
    public class RequestParser : IRequestParser
    {
        private readonly IHttpRequestFactory requestFactory;

        public RequestParser(IHttpRequestFactory requestFactory)
        {
            this.requestFactory = requestFactory;
        }

        public IHttpRequest Parse(string requestAsString)
        {
            StringReader textReader = new StringReader(requestAsString);
            string firstLine = textReader.ReadLine();
            IHttpRequest requestObject = this.CreateRequest(firstLine);

            string line;
            while ((line = textReader.ReadLine()) != null)
            {
                this.AddHeaderToRequest(requestObject, line);
            }

            return requestObject;
        }

        private IHttpRequest CreateRequest(string firstRequestLine)
        {
            string[] firstRequestLineParts = firstRequestLine.Split(' ');
            if (firstRequestLineParts.Length != 3)
            {
                throw new ParserException(
                    "Invalid format for the first request line. Expected format: [Method] [Uri] HTTP/[Version]");
            }

            IHttpRequest requestObject = this.requestFactory.CreateHttpRequest(
                                                            firstRequestLineParts[0],
                                                            firstRequestLineParts[1],
                                                            firstRequestLineParts[2]);

            return requestObject;
        }

        private void AddHeaderToRequest(IHttpRequest request, string headerLine)
        {
            string[] headerParts = headerLine.Split(new[] { ':' }, 2);
            string headerName = headerParts[0].Trim();
            string headerValue = headerParts.Length == 2 ? headerParts[1].Trim() : string.Empty;
            request.AddHeader(headerName, headerValue);
        }
    }
}