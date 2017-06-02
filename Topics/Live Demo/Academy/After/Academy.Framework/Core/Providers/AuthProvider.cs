using Academy.Core.Contracts;

namespace Academy.Core.Providers
{
    public class AuthProvider : IAuthProvider
    {
        public bool IsUserAuth()
        {
            // Execute auth logic

            return true;
        }
    }
}