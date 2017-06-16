using System;
using System.Collections.Generic;
using SchoolSystem.Framework.Core.Commands.Contracts;

namespace SchoolSystem.Framework.Core.Commands
{
    public class RemoveStudentCommand : ICommand
    {
        public string Execute(IList<string> parameters)
        {
            var studentId = int.Parse(parameters[0]);
            Engine.Students.Remove(studentId);
            return $"Student with ID {studentId} was sucessfully removed.";
        }
    }
}
