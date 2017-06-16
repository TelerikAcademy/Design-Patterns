using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Common
{
    public class DefaultDateTimeProvider : DateTimeProvider
    {
        private static DefaultDateTimeProvider instance = new DefaultDateTimeProvider();

        private DefaultDateTimeProvider()
        {
        }

        public static DefaultDateTimeProvider Instance
        {
            get
            {
                return instance;
            }
        }

        public override DateTime UtcNow
        {
            get
            {
                return DateTime.Now;
            }
        }
    }
}
