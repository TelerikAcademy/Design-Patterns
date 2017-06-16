using Moq;
using NUnit.Framework;
using SchoolSystem.Framework.Core.Commands;
using SchoolSystem.Framework.Models.Contracts;
using System.Collections.Generic;

namespace SchoolSystem.Tests.Core.Commands.RemoveStudentCommandTests
{
    [TestFixture]
    public class Execute_Should
    {
        private static readonly string SuccessMessageTemplate = "Student with ID {0} was sucessfully removed.";

        private const string SourcesListVariableName = "_sourceLists";

        private static object[] _sourceLists = {new object[] {new List<string> { "0" }},
                                                new object[] {new List<string> { "1" }} };

        [Test, TestCaseSource(SourcesListVariableName)]
        public void RemoveStudentWithCorrectId_WhenParametersAreCorrect(IList<string> parameters)
        {
            // Arrange
            var studentId = int.Parse(parameters[0]);
            var mockRemoveStudent = new Mock<IRemoveStudent>();
            var removeStudentCommand = new RemoveStudentCommand(mockRemoveStudent.Object);

            // Act
            removeStudentCommand.Execute(parameters);

            // Assert
            mockRemoveStudent.Verify(a => a.RemoveStudent(studentId), Times.Once());
        }

        [Test, TestCaseSource(SourcesListVariableName)]
        public void ReturnSuccessMessage_WhenParametersAreCorrect(IList<string> parameters)
        {
            // Arrange
            var studentId = int.Parse(parameters[0]);
            var mockRemoveStudent = new Mock<IRemoveStudent>();
            var expectedResult = string.Format(SuccessMessageTemplate, studentId);
            var removeStudentCommand = new RemoveStudentCommand(mockRemoveStudent.Object);

            // Act
            var result = removeStudentCommand.Execute(parameters);

            // Assert
            Assert.AreEqual(expectedResult, result);
        }
    }
}