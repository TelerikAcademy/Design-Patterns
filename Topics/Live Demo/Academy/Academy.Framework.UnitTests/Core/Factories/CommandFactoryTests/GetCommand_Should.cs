using Academy.Core.Factories;
using Academy.Framework.Core.Contracts;
using Moq;
using NUnit.Framework;
using System;

namespace Academy.Framework.UnitTests.Core.Factories.CommandFactoryTests
{
    [TestFixture]
    public class GetCommand_Should
    {
        [Test]
        public void ThrowArgumentNullException_WhenArgumentIsNull()
        {
            // Arrange
            var serviceLocatorMock = new Mock<IServiceLocator>();
            var commandFactory = new CommandFactory(serviceLocatorMock.Object);

            // Act && Assert
            Assert.Throws<ArgumentNullException>(() => commandFactory.GetCommand(null));
        }

        [Test]
        public void ThrowArgumentException_WhenArgumentIsEmpty()
        {
            // Arrange
            var serviceLocatorMock = new Mock<IServiceLocator>();
            var commandFactory = new CommandFactory(serviceLocatorMock.Object);

            // Act && Assert
            Assert.Throws<ArgumentException>(() => commandFactory.GetCommand(string.Empty));
        }

        [TestCase("CreateSeason 2016 2017 SoftwareAcademy", "CreateSeason")]
        [TestCase("CreateStudent Pesho Frontend", "CreateStudent")]
        [TestCase("AddStudentToSeason Pesho 0", "AddStudentToSeason")]
        public void CallGetCommand_WhenArgumentIsValidCommand(string fullCommand, string commandName)
        {
            // Arrange
            var serviceLocatorMock = new Mock<IServiceLocator>();
            var commandFactory = new CommandFactory(serviceLocatorMock.Object);

            // Act
            commandFactory.GetCommand(fullCommand);

            // Assert
            serviceLocatorMock.Verify(m => m.GetCommand(commandName), Times.Once());
        }
    }
}
