using Academy.Core.Contracts;
using Ninject.Extensions.Interception;

namespace Academy.Interceptors
{
    public class AuthInterceptor : IInterceptor
    {
        private readonly IAuthProvider authProvider;

        public AuthInterceptor(IAuthProvider authProvider)
        {
            this.authProvider = authProvider;
        }

        public void Intercept(IInvocation invocation)
        {
            if (this.authProvider.IsUserAuth())
            {
                invocation.Proceed();
            }
        }
    }
}