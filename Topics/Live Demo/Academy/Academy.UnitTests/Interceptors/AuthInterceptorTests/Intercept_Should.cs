using Academy.Core.Contracts;
using Academy.Interceptors;
using Moq;
using Ninject.Extensions.Interception;
using NUnit.Framework;

namespace Academy.UnitTests.Interceptors.AuthInterceptorTests
{
    [TestFixture]
    public class Intercept_Should
    {
        [Test]
        public void CallProceed_WhenUserIsAuth()
        {
            // Arrange
            var authProviderMock = new Mock<IAuthProvider>();
            var invocationMock = new Mock<IInvocation>();

            authProviderMock.Setup(m => m.IsUserAuth()).Returns(true);

            var authInterceptor = new AuthInterceptor(authProviderMock.Object);

            // Act
            authInterceptor.Intercept(invocationMock.Object);

            // Assert
            invocationMock.Verify(i => i.Proceed(), Times.Once());
        }

        [Test]
        public void NotCallProceed_WhenUserIsNotAuth()
        {
            // Arrange
            var authProviderMock = new Mock<IAuthProvider>();
            var invocationMock = new Mock<IInvocation>();

            authProviderMock.Setup(m => m.IsUserAuth()).Returns(false);

            var authInterceptor = new AuthInterceptor(authProviderMock.Object);

            // Act
            authInterceptor.Intercept(invocationMock.Object);

            // Assert
            invocationMock.Verify(i => i.Proceed(), Times.Never());
        }
    }
}
