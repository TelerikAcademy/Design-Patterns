using Academy.Commands.Contracts;
using Academy.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Academy.Commands.Adding
{
    public class AddStudentToCourseCommand : ICommand
    {
        private readonly IAcademyFactory factory;
        private readonly IEngine engine;

        public AddStudentToCourseCommand(IAcademyFactory factory, IEngine engine)
        {
            this.factory = factory;
            this.engine = engine;
        }

        public string Execute(IList<string> parameters)
        {
            var studentUsername = parameters[0];
            var seasonId = parameters[1];
            var courseId = parameters[2];
            var form = parameters[3];

            var student = this.engine.Students.Single(x => x.Username.ToLower() == studentUsername.ToLower());
            var course = this.engine
                .Seasons[int.Parse(seasonId)]
                .Courses[int.Parse(courseId)];

            switch (form.ToLower())
            {
                case "onsite":
                    course.OnsiteStudents.Add(student);
                    break;
                case "online":
                    course.OnlineStudents.Add(student);
                    break;
                default:
                    throw new ArgumentException($"Cannot add student to course {seasonId}.{course.Name}. Invalid course form {form}!");
            }

            return $"Student {studentUsername} was added to Course {seasonId}.{course.Name}.";
        }
    }
}
