using System;
using System.Collections.Generic;

using Moq;
using NUnit.Framework;
using ProjectManager.Framework.Core.Commands.Contracts;
using ProjectManager.Framework.Core.Commands.Decorators;
using ProjectManager.Framework.Services;

namespace ProjectManager.Tests.ProjectManager.Framework.Core.Commands.Decorators.CacheableCommandTests
{
    [TestFixture]
    public class Execute_Should
    {
        [Test]
        public void ThrowsArgumentNullExceptionWhenParametersAreNull()
        {
            // Arrange
            var decoratedCommandMock = new Mock<ICommand>();
            var cachingServiceMock = new Mock<ICachingService>();

            var command = new CacheableCommand(decoratedCommandMock.Object, cachingServiceMock.Object);

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => command.Execute(null));
        }

        [Test]
        public void ResetTheCacheAndExecuteDecoratedCommandWhenTheCacheIsExpired()
        {
            // Arrange
            var decoratedCommandMock = new Mock<ICommand>();
            var cachingServiceMock = new Mock<ICachingService>();

            cachingServiceMock.SetupGet(x => x.IsExpired).Returns(true);
            cachingServiceMock.Setup(x => x.AddCacheValue(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<object>()));
            cachingServiceMock.Setup(x => x.ResetCache());

            decoratedCommandMock.Setup(x => x.Execute(It.IsAny<List<string>>())).Returns("result");

            var command = new CacheableCommand(decoratedCommandMock.Object, cachingServiceMock.Object);

            // Act
            var result = command.Execute(new List<string>());

            // Assert
            cachingServiceMock.Verify(x => x.ResetCache(), Times.Once);
            cachingServiceMock.Verify(x => x.AddCacheValue(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<object>()), Times.Once);
            decoratedCommandMock.Verify(x => x.Execute(It.IsAny<List<string>>()), Times.Once);
            Assert.AreEqual("result", result);
        }

        [Test]
        public void GetCachedValueWhenTheCacheIsNotExpired()
        {
            // Arrange
            var decoratedCommandMock = new Mock<ICommand>();
            var cachingServiceMock = new Mock<ICachingService>();

            cachingServiceMock.SetupGet(x => x.IsExpired).Returns(false);
            cachingServiceMock.Setup(x => x.GetCacheValue(It.IsAny<string>(), It.IsAny<string>())).Returns("result");

            var command = new CacheableCommand(decoratedCommandMock.Object, cachingServiceMock.Object);

            // Act
            var result = command.Execute(new List<string>());

            // Assert
            cachingServiceMock.Verify(x => x.GetCacheValue(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
            Assert.AreEqual("result", result);
        }
    }
}
