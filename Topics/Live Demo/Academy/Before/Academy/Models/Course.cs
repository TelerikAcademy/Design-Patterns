using Academy.Models.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Academy.Models
{
    public class Course : ICourse
    {
        private string name;
        private int lecturesPerWeek;
        private DateTime startingDate;
        private DateTime endingDate;

        public Course(string name, int lecturesPerWeek, DateTime starting, DateTime ending)
        {
            this.Name = name;
            this.LecturesPerWeek = lecturesPerWeek;
            this.StartingDate = starting;
            this.EndingDate = ending;

            this.OnlineStudents = new List<IStudent>();
            this.OnsiteStudents = new List<IStudent>();
            this.Lectures = new List<ILecture>();
        }

        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value) 
                    || value.Length < 3 
                    || value.Length > 45)
                {
                    throw new ArgumentException("The name of the course must be between 3 and 45 symbols!");
                }

                this.name = value;
            }
        }

        public int LecturesPerWeek
        {
            get
            {
                return this.lecturesPerWeek;
            }
            set
            {
                if (value < 1 || value > 7)
                {
                    throw new ArgumentException("The number of lectures per week must be between 1 and 7!");
                }

                this.lecturesPerWeek = value;
            }
        }

        public DateTime StartingDate
        {
            get
            {
                return this.startingDate;
            }
            set
            {
                if (value == null)
                {
                    throw new ArgumentException("The course starting date must be valid!");
                }

                this.startingDate = value;
            }
        }

        public DateTime EndingDate
        {
            get
            {
                return this.endingDate;
            }
            set
            {
                if (value == null)
                {
                    throw new ArgumentException("The course ending date must be valid!");
                }

                this.endingDate = value;
            }
        }

        public IList<IStudent> OnsiteStudents { get; private set; }

        public IList<IStudent> OnlineStudents { get; private set; }

        public IList<ILecture> Lectures { get; private set; }

        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.AppendLine($"* Course:");
            builder.AppendLine($" - Name: {this.Name}");
            builder.AppendLine($" - Lectures per week: {this.lecturesPerWeek}");
            builder.AppendLine($" - Starting date: {this.StartingDate}");
            builder.AppendLine($" - Ending date: {this.EndingDate}");
            builder.AppendLine($" - Onsite students: {this.OnsiteStudents.Count}");
            builder.AppendLine($" - Online students: {this.OnlineStudents.Count}");
            builder.AppendLine($" - Lectures:");

            if (this.Lectures.Any())
            {
                foreach (var result in this.Lectures)
                {
                    builder.AppendLine(result.ToString());
                }
            }
            else
            {
                builder.AppendLine("  * There are no lectures in this course!");
            }

            return builder.ToString().TrimEnd();
        }
    }
}
