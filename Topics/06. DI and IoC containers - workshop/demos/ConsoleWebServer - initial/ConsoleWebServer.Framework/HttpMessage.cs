namespace ConsoleWebServer.Framework
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public abstract class HttpMessage
    {
        protected const string HttpVersionPrefix = "HTTP/";

        protected HttpMessage(string httpVersion)
        {
            this.ProtocolVersion = Version.Parse(httpVersion.ToLower().Replace(HttpVersionPrefix.ToLower(), string.Empty));
            this.Headers = new SortedDictionary<string, ICollection<string>>();
        }

        public Version ProtocolVersion { get; private set; }

        public IDictionary<string, ICollection<string>> Headers { get; private set; }

        public void AddHeader(string name, string value)
        {
            if (!this.Headers.ContainsKey(name))
            {
                this.Headers.Add(name, new HashSet<string>());
            }

            this.Headers[name].Add(value);
        }

        public override string ToString()
        {
            var stringBuilder = new StringBuilder();
            foreach (var key in this.Headers.Keys)
            {
                stringBuilder.AppendLine(string.Format("{0}: {1}", key, string.Join("; ", this.Headers[key])));
            }

            return stringBuilder.ToString();
        }
    }
}