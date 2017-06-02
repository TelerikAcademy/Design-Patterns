using Academy.Models.Abstractions;
using Academy.Models.Contracts;
using System.Collections.Generic;
using System.Text;

namespace Academy.Models
{
    public class Trainer : User, ITrainer
    {
        public Trainer(string username, IList<string> technologies) : base(username)
        {
            this.Technologies = technologies;
        }

        public IList<string> Technologies { get; set; }

        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.AppendLine("* Trainer:");
            builder.AppendLine($" - Username: {this.Username}");
            builder.AppendLine($" - Technologies: {string.Join("; ", this.Technologies)}");

            return builder.ToString().TrimEnd();
        }
    }
}
