using Ninject.Extensions.Interception;
using SchoolSystem.Framework.Core.Contracts;
using System.Diagnostics;

namespace SchoolSystem.Cli.Interceptors
{
    public class StopwatchInterceptor : IInterceptor
    {
        private readonly IWriter writer;
        private readonly Stopwatch stopwatch;

        public StopwatchInterceptor(IWriter writer)
        {
            this.writer = writer;
            this.stopwatch = new Stopwatch();
        }

        public void Intercept(IInvocation invocation)
        {
            this.writer.WriteLine($"Calling method {invocation.Request.Method.Name} of type {invocation.Request.Method.DeclaringType.Name}...");

            this.stopwatch.Start();
            invocation.Proceed();
            this.stopwatch.Stop();

            this.writer.WriteLine($"Total execution time for method {invocation.Request.Method.Name} of type {invocation.Request.Method.DeclaringType.Name} is {this.stopwatch.ElapsedMilliseconds} milliseconds.");
        }
    }
}