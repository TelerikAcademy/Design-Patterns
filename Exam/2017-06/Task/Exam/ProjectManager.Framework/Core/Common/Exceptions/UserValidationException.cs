using System;

namespace ProjectManager.Framework.Core.Common.Exceptions
{
    public class UserValidationException : Exception
    {
        public UserValidationException(string message) 
            : base(message)
        {
        }
    }
}
