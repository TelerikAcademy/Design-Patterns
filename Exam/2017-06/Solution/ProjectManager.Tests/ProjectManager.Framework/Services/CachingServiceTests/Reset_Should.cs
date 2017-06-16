using System;

using Moq;
using NUnit.Framework;
using ProjectManager.Common;
using ProjectManager.Tests.ProjectManager.Framework.Services.CachingServiceTests.Mocks;
using System.Collections.Generic;

namespace ProjectManager.Tests.ProjectManager.Framework.Services.CachingServiceTests
{
    [TestFixture]
    public class Reset_Should
    {
        [Test]
        public void AddProperValueToTheTimeWhenInvoked()
        {
            // Arrange
            var returnDate = new DateTime(2017, 6, 15, 15, 00, 00);

            var dateTimeMock = new Mock<DateTimeProvider>();
            dateTimeMock.SetupGet(x => x.UtcNow).Returns(returnDate);

            DateTimeProvider.Current = dateTimeMock.Object;

            var cachingService = new CachingServiceMock(TimeSpan.FromSeconds(20));

            // Act
            cachingService.ResetCache();

            // Assert
            Assert.AreEqual(returnDate.AddSeconds(20), cachingService.DateTimeExpiring);
        }

        [Test]
        public void CreateNewDictionaryWhenInvoked()
        {
            // Arrange
            var cachingService = new CachingServiceMock(It.IsAny<TimeSpan>());

            // Act
            cachingService.ResetCache();

            // Assert
            Assert.IsInstanceOf(typeof(Dictionary<string, object>), cachingService.CacheStorage);
            Assert.AreEqual(0, cachingService.CacheStorage.Count);
        }

        [TearDown]
        public void ResetDateTimeProvider()
        {
            DateTimeProvider.ResetToDefault();
        }
    }
}
