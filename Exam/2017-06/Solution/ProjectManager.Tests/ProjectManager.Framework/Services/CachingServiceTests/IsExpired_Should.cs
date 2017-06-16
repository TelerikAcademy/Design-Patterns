using System;

using Moq;
using NUnit.Framework;
using ProjectManager.Common;
using ProjectManager.Framework.Services;

namespace ProjectManager.Tests.ProjectManager.Framework.Services.CachingServiceTests
{
    [TestFixture]
    public class IsExpired_Should
    {
        [Test]
        public void ReturnsTrueIfCurrentDateIsBiggerThanTimeExpire()
        {
            // Arrange
            var currentDate = new DateTime(2017, 6, 15, 15, 00, 00);

            var dateTimeMock = new Mock<DateTimeProvider>();
            dateTimeMock.SetupGet(x => x.UtcNow).Returns(currentDate);

            DateTimeProvider.Current = dateTimeMock.Object;

            var cachingService = new CachingService(TimeSpan.FromSeconds(20));

            dateTimeMock.SetupGet(x => x.UtcNow).Returns(new DateTime(2017, 6, 15, 15, 00, 10));
            DateTimeProvider.Current = dateTimeMock.Object;

            // Act
            var result = cachingService.IsExpired;

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void ReturnsFalseIfCurrentDateIsSmallerThanTimeExpire()
        {
            // Arrange
            var currentDate = new DateTime(2017, 6, 15, 15, 00, 00);

            var dateTimeMock = new Mock<DateTimeProvider>();
            dateTimeMock.SetupGet(x => x.UtcNow).Returns(currentDate);

            DateTimeProvider.Current = dateTimeMock.Object;

            var cachingService = new CachingService(TimeSpan.FromSeconds(20));

            dateTimeMock.SetupGet(x => x.UtcNow).Returns(new DateTime(2017, 6, 15, 14, 59, 50));
            DateTimeProvider.Current = dateTimeMock.Object;

            // Act
            var result = cachingService.IsExpired;

            // Assert
            Assert.IsFalse(result);
        }

        [TearDown]
        public void ResetDateTimeProvider()
        {
            DateTimeProvider.ResetToDefault();
        }
    }
}
