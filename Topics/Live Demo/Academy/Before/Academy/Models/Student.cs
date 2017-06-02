using Academy.Models.Abstractions;
using Academy.Models.Contracts;
using Academy.Models.Enums;
using Academy.Models.Utils.Contracts;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Academy.Models
{
    public class Student : User, IStudent
    {
        public Student(string username, Track track) : base(username)
        {
            this.Track = track;
            this.CourseResults = new List<ICourseResult>();
        }

        public Track Track { get; set; }

        public IList<ICourseResult> CourseResults { get; set; }

        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.AppendLine($"* Student:");
            builder.AppendLine($" - Username: {this.Username}");
            builder.AppendLine($" - Track: {this.Track}");
            builder.AppendLine($" - Course results:");

            if (this.CourseResults.Any())
            {
                foreach (var result in this.CourseResults)
                {
                    builder.AppendLine(result.ToString());
                }
            }
            else
            {
                builder.AppendLine("  * User has no course results!");
            }            

            return builder.ToString().TrimEnd();
        }
    }
}
