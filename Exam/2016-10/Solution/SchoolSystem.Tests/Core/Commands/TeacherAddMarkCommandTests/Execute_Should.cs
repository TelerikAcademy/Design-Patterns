using Moq;
using NUnit.Framework;
using SchoolSystem.Framework.Core.Commands;
using SchoolSystem.Framework.Models.Contracts;
using SchoolSystem.Framework.Models.Enums;
using System;
using System.Collections.Generic;

namespace SchoolSystem.Tests.Core.Commands.TeacherAddMarkCommandTests
{
    [TestFixture]
    public class Execute_Should
    {
        private static readonly string SuccessMessageTemplate = "Teacher {0} {1} added mark {2} to student {3} {4} in {5}.";

        private const string SourcesListVariableName = "_sourceLists";

        private static object[] _sourceLists = {new object[] {new List<string> { "0", "1", "4" }},
                                                new object[] {new List<string> { "1", "0", "3" } }};

        [Test, TestCaseSource(SourcesListVariableName)]
        public void AddMarkToStudent_WhenParametersAreCorrect(IList<string> parameters)
        {
            // Arrange
            var parametersTuple = GetParameters(parameters);
            var studentMock = new Mock<IStudent>();
            var teacherMock = new Mock<ITeacher>();

            var getStudentAndTeacherMock = new Mock<IGetStudentAndTeacher>();
            getStudentAndTeacherMock.Setup(g => g.GetStudent(parametersTuple.Item2)).Returns(studentMock.Object);
            getStudentAndTeacherMock.Setup(g => g.GetTeacher(parametersTuple.Item1)).Returns(teacherMock.Object);

            var teacherAddMarkCommand = new TeacherAddMarkCommand(getStudentAndTeacherMock.Object);

            // Act
            teacherAddMarkCommand.Execute(parameters);

            // Assert
            teacherMock.Verify(t => t.AddMark(studentMock.Object, parametersTuple.Item3), Times.Once());
        }

        [Test, TestCaseSource(SourcesListVariableName)]
        public void ReturnSuccessMessage_WhenParametersAreCorrect(IList<string> parameters)
        {
            // Arrange
            var parametersTuple = GetParameters(parameters);

            var studentFirstName = "studentFirstName";
            var studentLastName = "studentLastName";
            var studentMock = new Mock<IStudent>();
            studentMock.SetupGet(s => s.FirstName).Returns(studentFirstName);
            studentMock.SetupGet(s => s.LastName).Returns(studentLastName);

            var teacherFirstName = "teacherFirstName";
            var teacherLastName = "teacherLastName";
            var teacherSubject = Subject.English;
            var teacherMock = new Mock<ITeacher>();
            teacherMock.SetupGet(s => s.FirstName).Returns(teacherFirstName);
            teacherMock.SetupGet(s => s.LastName).Returns(teacherLastName);
            teacherMock.SetupGet(s => s.Subject).Returns(teacherSubject);

            var getStudentAndTeacherMock = new Mock<IGetStudentAndTeacher>();
            getStudentAndTeacherMock.Setup(g => g.GetStudent(parametersTuple.Item2)).Returns(studentMock.Object);
            getStudentAndTeacherMock.Setup(g => g.GetTeacher(parametersTuple.Item1)).Returns(teacherMock.Object);

            var expectedResult = string.Format(SuccessMessageTemplate, teacherFirstName, teacherLastName, 
                                               parametersTuple.Item3, studentFirstName, studentLastName, teacherSubject);
            var teacherAddMarkCommand = new TeacherAddMarkCommand(getStudentAndTeacherMock.Object);

            // Act
            var result = teacherAddMarkCommand.Execute(parameters);

            // Assert
            Assert.AreEqual(expectedResult, result);
        }

        private static Tuple<int, int, float> GetParameters(IList<string> parameters)
        {
            return new Tuple<int, int, float>(int.Parse(parameters[0]), int.Parse(parameters[1]), float.Parse(parameters[2]));
        }
    }
}