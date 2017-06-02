using Academy.Core.Contracts;
using Ninject.Extensions.Interception;
using System.Diagnostics;

namespace Academy.Interceptors
{
    public class StopwatchInterceptor : IInterceptor
    {
        private readonly ILogger logger;

        public StopwatchInterceptor(ILogger logger)
        {
            this.logger = logger;
        }

        public void Intercept(IInvocation invocation)
        {
            Stopwatch stopwatch = new Stopwatch();

            stopwatch.Start();
            invocation.Proceed();
            stopwatch.Stop();

            this.logger.Log(string.Format("Type {0} is called with method {1} and run for {2} ticks.",
                invocation.Request.Target.GetType().Name,
                invocation.Request.Method.Name,
                stopwatch.ElapsedTicks));
        }
    }
}