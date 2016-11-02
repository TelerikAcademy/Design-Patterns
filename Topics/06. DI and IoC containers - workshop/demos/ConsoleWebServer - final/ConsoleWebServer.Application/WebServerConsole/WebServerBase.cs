using ConsoleWebServer.Framework;
using System.Text;

namespace ConsoleWebServer.Application.WebServerConsole
{
    public class WebServerBase<T> : IWebServerConsole
        where T : IInputOutputProvider
    {
        private readonly IResponseProvider responseProvider;
        private readonly T inputOutputProvider;

        public WebServerBase(IResponseProvider responseProvider, T inputOutputProvider)
        {
            this.responseProvider = responseProvider;
            this.inputOutputProvider = inputOutputProvider;
        }

        protected T InputOutputProvider
        {
            get
            {
                return this.inputOutputProvider;
            }
        }

        public void Start()
        {
            StringBuilder requestBuilder = new StringBuilder();
            string inputLine;
            while ((inputLine = this.InputOutputProvider.ReadInput()) != null)
            {
                if (string.IsNullOrWhiteSpace(inputLine))
                {
                    IHttpResponse response = this.responseProvider.GetResponse(requestBuilder.ToString());
                    this.WriteOutput(response.ToString());
                    requestBuilder.Clear();
                    continue;
                }

                requestBuilder.AppendLine(inputLine);
            }
        }

        protected virtual void WriteOutput(string value)
        {
            this.InputOutputProvider.WriteOutput(value);
        }
    }
}