using Bytes2you.Validation;
using Ninject.Extensions.Interception;
using ProjectManager.Framework.Core.Common.Contracts;

namespace ProjectManager.ConsoleClient.Interceptors
{
    public class InfoInterceptor : SimpleInterceptor
    {
        private readonly IWriter writer;

        public InfoInterceptor(IWriter writer)
        {
            Guard.WhenArgument(writer, "writer").IsNull().Throw();

            this.writer = writer;
        }
        protected override void AfterInvoke(IInvocation invocation)
        {
            this.writer.WriteLine($"{invocation.ReturnValue}");
        }
    }
}
