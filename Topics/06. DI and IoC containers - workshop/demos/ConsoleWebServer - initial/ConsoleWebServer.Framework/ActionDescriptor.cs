namespace ConsoleWebServer.Framework
{
    using System;

    public class ActionDescriptor
    {
        public const string DefaultControllerName = "Home";

        public const string DefaultActionName = "Index";

        public ActionDescriptor(string uri)
        {
            uri = uri ?? string.Empty;

            var uriParts = uri.Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries);

            this.ControllerName = uriParts.Length > 0 ? uriParts[0] : DefaultControllerName;

            this.ActionName = uriParts.Length > 1 ? uriParts[1] : DefaultActionName;

            this.Parameter = uriParts.Length > 2 ? uriParts[2] : string.Empty;
        }

        public string ControllerName { get; private set; }

        public string ActionName { get; private set; }

        public string Parameter { get; private set; }

        public override string ToString()
        {
            return string.Format("/{0}/{1}/{2}", this.ControllerName, this.ActionName, this.Parameter);
        }
    }
}
