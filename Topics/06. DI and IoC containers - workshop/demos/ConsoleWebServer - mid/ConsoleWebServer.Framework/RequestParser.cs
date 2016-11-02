namespace ConsoleWebServer.Framework
{
    using System.IO;

    public class RequestParser
    {
        public IHttpRequest Parse(string requestAsString)
        {
            var textReader = new StringReader(requestAsString);
            var firstLine = textReader.ReadLine();
            var requestObject = this.CreateRequest(firstLine);

            string line;
            while ((line = textReader.ReadLine()) != null)
            {
                this.AddHeaderToRequest(requestObject, line);
            }

            return requestObject;
        }

        private IHttpRequest CreateRequest(string firstRequestLine)
        {
            var firstRequestLineParts = firstRequestLine.Split(' ');
            if (firstRequestLineParts.Length != 3)
            {
                throw new ParserException(
                    "Invalid format for the first request line. Expected format: [Method] [Uri] HTTP/[Version]");
            }

            var requestObject = new HttpRequest(
                firstRequestLineParts[0],
                firstRequestLineParts[1],
                firstRequestLineParts[2]);

            return requestObject;
        }

        private void AddHeaderToRequest(IHttpRequest request, string headerLine)
        {
            var headerParts = headerLine.Split(new[] { ':' }, 2);
            var headerName = headerParts[0].Trim();
            var headerValue = headerParts.Length == 2 ? headerParts[1].Trim() : string.Empty;
            request.AddHeader(headerName, headerValue);
        }
    }
}