namespace ConsoleWebServer.Framework
{
    using System.Text;

    public class HttpRequest : HttpMessage, IHttpRequest
    {
        public HttpRequest(string method, string uri, string httpVersion)
            : base(httpVersion)
        {
            this.Uri = uri;
            this.Method = method;
            this.Action = new ActionDescriptor(uri);
        }

        public string Uri { get; private set; }

        public string Method { get; private set; }

        public ActionDescriptor Action { get; private set; }

        public override string ToString()
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine(
                string.Format(
                    "{0} {1} {2}{3}",
                    this.Method,
                    this.Action,
                    HttpMessage.HttpVersionPrefix,
                    this.ProtocolVersion));
            stringBuilder.AppendLine(base.ToString().Trim());
            return stringBuilder.ToString();
        }
    }
}