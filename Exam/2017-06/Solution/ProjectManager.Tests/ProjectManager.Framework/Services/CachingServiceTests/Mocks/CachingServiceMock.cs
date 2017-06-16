using System;
using System.Collections.Generic;

using ProjectManager.Framework.Services;

namespace ProjectManager.Tests.ProjectManager.Framework.Services.CachingServiceTests.Mocks
{
    public class CachingServiceMock : CachingService
    {
        public CachingServiceMock(TimeSpan duration)
            : base(duration)
        {
        }

        public IDictionary<string, object> CacheStorage
        {
            get
            {
                return this.Cache;
            }
        }

        public DateTime DateTimeExpiring
        {
            get
            {
                return this.TimeExpiring;
            }
        }
    }
}
