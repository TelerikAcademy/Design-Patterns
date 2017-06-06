using Academy.Core.Contracts;
using Bytes2you.Validation;
using Ninject.Extensions.Interception;

namespace Academy.Interceptors
{
    public class AuthInterceptor : IInterceptor
    {
        private readonly IAuthProvider authProvider;

        public AuthInterceptor(IAuthProvider authProvider)
        {
            Guard.WhenArgument(authProvider, "authProvider").IsNull().Throw();

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