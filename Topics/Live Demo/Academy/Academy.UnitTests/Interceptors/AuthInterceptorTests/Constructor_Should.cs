using Academy.Core.Contracts;
using Academy.Interceptors;
using Moq;
using NUnit.Framework;
using System;

namespace Academy.UnitTests.Interceptors.AuthInterceptorTests
{
    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void ThrowException_WhenArgumentIsNull()
        {
            // Arrange & Act & Assert
            Assert.Throws<ArgumentNullException>(() => new AuthInterceptor(null));
        }

        [Test]
        public void NotThrowException_WhenArgumentIsNotNull()
        {
            // Arrange & Act & Assert
            Assert.DoesNotThrow(() => new AuthInterceptor(new Mock<IAuthProvider>().Object));
        }
    }
}