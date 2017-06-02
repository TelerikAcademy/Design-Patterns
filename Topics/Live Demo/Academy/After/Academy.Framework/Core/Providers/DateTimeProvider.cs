using System;

namespace Academy.Core.Providers
{
    internal abstract class DateTimeProvider
    {
        public static DateTime Now
        {
            get
            {
                return new DateTime(2017, 1, 16, 0, 0, 0);
            }
        }
    }
}
