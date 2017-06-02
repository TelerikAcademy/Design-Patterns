using Academy.Commands.Contracts;
using Academy.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Academy.Commands.Creating
{
    public class CreateStudentCommand : ICommand
    {
        private readonly IAcademyFactory factory;
        private readonly IAcademyDatabase academyDatabase;

        public CreateStudentCommand(IAcademyFactory factory, IAcademyDatabase academyDatabase)
        {
            this.factory = factory;
            this.academyDatabase= academyDatabase;
        }

        public string Execute(IList<string> parameters)
        {
            var username = parameters[0];
            var track = parameters[1];

            if (this.academyDatabase.Students.Any(x => x.Username.ToLower() == username.ToLower()) ||
                this.academyDatabase.Trainers.Any(x => x.Username.ToLower() == username.ToLower()))
            {
                throw new ArgumentException($"A user with the username {username} already exists!");
            }

            var student = this.factory.CreateStudent(username, track);
            this.academyDatabase.Students.Add(student);

            return $"Student with ID {this.academyDatabase.Students.Count - 1} was created.";
        }
    }
}
