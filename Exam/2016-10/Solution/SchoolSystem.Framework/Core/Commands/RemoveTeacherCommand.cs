using SchoolSystem.Framework.Core.Commands.Contracts;
using SchoolSystem.Framework.Models.Contracts;
using System.Collections.Generic;

namespace SchoolSystem.Framework.Core.Commands
{
    public class RemoveTeacherCommand : ICommand
    {
        private readonly IRemoveTeacher removeTeacher;

        public RemoveTeacherCommand(IRemoveTeacher removeTeacher)
        {
            this.removeTeacher = removeTeacher;
        }

        public string Execute(IList<string> parameters)
        {
            var teacherId = int.Parse(parameters[0]);
            this.removeTeacher.RemoveTeacher(teacherId);
            return $"Teacher with ID {teacherId} was sucessfully removed.";
        }
    }
}