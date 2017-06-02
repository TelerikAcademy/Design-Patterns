using Academy.Models.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Academy.Models
{
    public class Lecture : ILecture
    {
        private string name;

        public Lecture(string name, DateTime date, ITrainer trainer)
        {
            this.Name = name;
            this.Date = date;
            this.Trainer = trainer;
            this.Resources = new List<ILectureResource>();
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
                    || value.Length < 5 
                    || value.Length > 30)
                {
                    throw new ArgumentException("Lecture's name should be between 5 and 30 symbols long!");
                }

                this.name = value;
            }
        }

        public DateTime Date { get; set; }

        public ITrainer Trainer { get; set; }

        public IList<ILectureResource> Resources { get; set; }

        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.AppendLine($"  * Lecture:");
            builder.AppendLine($"   - Name: {this.Name}");
            builder.AppendLine($"   - Date: {this.Date}");
            builder.AppendLine($"   - Trainer username: {this.Trainer.Username}");
            builder.AppendLine($"   - Resources:");

            if (this.Resources.Any())
            {
                foreach (var result in this.Resources)
                {
                    builder.Append(result.ToString());
                }
            }
            else
            {
                builder.Append("    * There are no resources in this lecture.");
            }

            return builder.ToString().TrimEnd();
        }
    }
}
