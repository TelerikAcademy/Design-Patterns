using System;

using Moq;
using Ninject.Extensions.Interception;
using NUnit.Framework;
using ProjectManager.ConsoleClient.Interceptors;
using ProjectManager.Framework.Core.Common.Contracts;
using ProjectManager.Framework.Core.Common.Exceptions;

namespace ProjectManager.Tests.ConsoleClient.Interceptors.LogErrorInterceptorTests
{
    [TestFixture]
    public class Intercept_Should
    {
        [Test]
        public void CallsProceedOnce()
        {
            // Arrange
            var loggerMock = new Mock<ILogger>();
            var writerMock = new Mock<IWriter>();

            var invocationMock = new Mock<IInvocation>();
            invocationMock.Setup(x => x.Proceed());

            var logErrorInterceptor = new LogErrorInterceptor(loggerMock.Object, writerMock.Object);

            // Act 
            logErrorInterceptor.Intercept(invocationMock.Object);

            // Assert
            invocationMock.Verify(l => l.Proceed(), Times.Once);
        }

        [Test]
        public void LogErrorAndWriteWhenUserValidationExceptionOccurs()
        {
            // Arrange
            var loggerMock = new Mock<ILogger>();
            var writerMock = new Mock<IWriter>();

            loggerMock.Setup(x => x.Error(It.IsAny<string>()));
            writerMock.Setup(x => x.WriteLine(It.IsAny<string>()));

            var invocationMock = new Mock<IInvocation>();

            var logErrorInterceptor = new LogErrorInterceptor(loggerMock.Object, writerMock.Object);

            invocationMock.Setup(x => x.Proceed()).Throws(new UserValidationException(It.IsAny<string>()));

            // Act
            logErrorInterceptor.Intercept(invocationMock.Object);

            // Assert
            loggerMock.Verify(l => l.Error(It.IsAny<string>()), Times.Once());
            writerMock.Verify(w => w.WriteLine(It.IsAny<string>()), Times.Once());
        }

        [Test]
        public void LogFatalAndWriteWhenUserValidationExceptionOccurs()
        {
            // Arrange
            var loggerMock = new Mock<ILogger>();
            var writerMock = new Mock<IWriter>();

            loggerMock.Setup(x => x.Fatal(It.IsAny<string>()));
            writerMock.Setup(x => x.WriteLine(It.IsAny<string>()));

            var invocationMock = new Mock<IInvocation>();

            var logErrorInterceptor = new LogErrorInterceptor(loggerMock.Object, writerMock.Object);

            invocationMock.Setup(x => x.Proceed()).Throws(new Exception(It.IsAny<string>()));

            // Act
            logErrorInterceptor.Intercept(invocationMock.Object);

            // Assert
            loggerMock.Verify(l => l.Fatal(It.IsAny<string>()), Times.Once());
            writerMock.Verify(w => w.WriteLine(It.IsAny<string>()), Times.Once());
        }
    }
}
