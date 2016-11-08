using Moq;
using NUnit.Framework;
using SchoolSystem.Framework.Core.Commands;
using SchoolSystem.Framework.Core.Contracts;
using SchoolSystem.Framework.Models.Contracts;
using SchoolSystem.Framework.Models.Enums;
using System;
using System.Collections.Generic;

namespace SchoolSystem.Tests.Core.Commands.CreateStudentCommandTests
{
    [TestFixture]
    public class Execute_Should
    {
        private static readonly string SuccessMessageTemplate = "A new student with name {0} {1}, grade {2} and ID {3} was created.";

        private const string SourcesListVariableName = "_sourceLists";

        private static object[] _sourceLists = {new object[] {new List<string> { "Pesho", "Petrov", "6" }},
                                                new object[] {new List<string> { "Gosho", "Peshev", "9" } }};

        [Test, TestCaseSource(SourcesListVariableName)]
        public void CreateStudentWithCorrectParametersByFactory_WhenParametersAreCorrect(IList<string> parameters)
        {
            // Arrange
            var parametersTuple = GetParameters(parameters);
            var mockStudentFactory = new Mock<IStudentFactory>();
            var mockAddStudent = new Mock<IAddStudent>();
            var createStudentCommand = new CreateStudentCommand(mockStudentFactory.Object, mockAddStudent.Object);

            // Act
            createStudentCommand.Execute(parameters);

            // Assert
            mockStudentFactory.Verify(a => a.CreateStudent(parametersTuple.Item1, parametersTuple.Item2, parametersTuple.Item3), Times.Once());
        }

        [Test, TestCaseSource(SourcesListVariableName)]
        public void AddStudentCreatedByFactory_WhenParametersAreCorrect(IList<string> parameters)
        {
            // Arrange
            var parametersTuple = GetParameters(parameters);
            var mockStudentFactory = new Mock<IStudentFactory>();
            var mockStudent = new Mock<IStudent>();
            mockStudentFactory.Setup(f => f.CreateStudent(parametersTuple.Item1, parametersTuple.Item2, parametersTuple.Item3)).Returns(mockStudent.Object);

            var mockAddStudent = new Mock<IAddStudent>();
            var createStudentCommand = new CreateStudentCommand(mockStudentFactory.Object, mockAddStudent.Object);

            // Act
            createStudentCommand.Execute(parameters);

            // Assert
            mockAddStudent.Verify(a => a.AddStudent(0, mockStudent.Object), Times.Once());
        }

        [Test]
        public void ReturnSuccessMessage_WhenParametersAreCorrect()
        {
            // Arrange
            var parametersForFirstCall = (List<string>)((object[])_sourceLists[0])[0];
            var parametersForSecondCall = (List<string>)((object[])_sourceLists[1])[0];
            var parametersTupleForFirstCall = GetParameters(parametersForFirstCall);
            var parametersTupleForSecondCall = GetParameters(parametersForSecondCall);
            var firstStudentId = 0;
            var secondStudentId = 1;

            var expectedResultForFirstCall =
                string.Format(SuccessMessageTemplate,
                              parametersTupleForFirstCall.Item1,
                              parametersTupleForFirstCall.Item2,
                              parametersTupleForFirstCall.Item3,
                              firstStudentId);

            var expectedResultForSecondCall =
                string.Format(SuccessMessageTemplate,
                              parametersTupleForSecondCall.Item1,
                              parametersTupleForSecondCall.Item2,
                              parametersTupleForSecondCall.Item3,
                              secondStudentId);

            var mockStudentFactory = new Mock<IStudentFactory>();
            var mockAddStudent = new Mock<IAddStudent>();
            var createStudentCommand = new CreateStudentCommand(mockStudentFactory.Object, mockAddStudent.Object);

            // Act
            var firstResult = createStudentCommand.Execute(parametersForFirstCall);
            var secondResult = createStudentCommand.Execute(parametersForSecondCall);

            // Assert
            Assert.AreEqual(expectedResultForFirstCall, firstResult);
            Assert.AreEqual(expectedResultForSecondCall, secondResult);
        }

        private static Tuple<string, string, Grade> GetParameters(IList<string> parameters)
        {
            return new Tuple<string, string, Grade>(parameters[0], parameters[1], (Grade)int.Parse(parameters[2]));
        }
    }
}