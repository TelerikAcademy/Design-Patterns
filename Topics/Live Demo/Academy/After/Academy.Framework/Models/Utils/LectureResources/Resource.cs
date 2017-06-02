using System;
using Academy.Models.Contracts;
using System.Text;

namespace Academy.Models.Utils.LectureResources
{
    public class Resource : ILectureResource
    {
        private string name;
        private string url;

        public Resource(string name, string url)
        {
            this.Name = name;
            this.Url = url;
        }

        public string Name
        {
            get
            {
                return this.name;
            }

            set
            {
                if (string.IsNullOrWhiteSpace(value) || value.Length < 3 || value.Length > 15)
                {
                    throw new ArgumentException("Resource name should be between 3 and 15 symbols long!");
                }

                this.name = value;
            }
        }

        public string Url
        {
            get
            {
                return this.url;
            }

            set
            {
                if (string.IsNullOrWhiteSpace(value) || value.Length < 5 || value.Length > 150)
                {
                    throw new ArgumentException("Resource url should be between 5 and 150 symbols long!");
                }

                this.url = value;
            }
        }

        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.AppendLine($"    * Resource:");
            builder.AppendLine($"     - Name: {this.Name}");
            builder.AppendLine($"     - Url: {this.Url}");

            return builder.ToString();
        }
    }
}
