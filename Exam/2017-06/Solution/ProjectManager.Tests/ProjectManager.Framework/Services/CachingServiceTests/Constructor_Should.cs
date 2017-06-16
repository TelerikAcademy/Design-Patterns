using System;

using Moq;
using NUnit.Framework;
using ProjectManager.Common;
using ProjectManager.Tests.ProjectManager.Framework.Services.CachingServiceTests.Mocks;
using System.Collections.Generic;

namespace ProjectManager.Tests.ProjectManager.Framework.Services.CachingServiceTests
{
    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void ThrowsWhenTheTimeSpanIsLessThanZero()
        {
            // Arrange, Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => new CachingServiceMock(TimeSpan.FromSeconds(-10.0)));
        }

        [Test]
        public void SetProperInitialDateTimeForExpiring()
        {
            // Arrange
            var returnDate = new DateTime(2017, 6, 15, 15, 00, 00);

            var dateTimeMock = new Mock<DateTimeProvider>();
            dateTimeMock.SetupGet(x => x.UtcNow).Returns(returnDate);

            DateTimeProvider.Current = dateTimeMock.Object;

            // Act
            var cachingService = new CachingServiceMock(TimeSpan.FromSeconds(It.IsAny<double>()));

            // Assert
            Assert.AreEqual(returnDate, cachingService.DateTimeExpiring);
        }

        [Test]
        public void InitilizeCacheWithEmptyDictionary()
        {
            // Act & Arrange

            var cachingService = new CachingServiceMock(TimeSpan.FromSeconds(It.IsAny<double>()));

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

