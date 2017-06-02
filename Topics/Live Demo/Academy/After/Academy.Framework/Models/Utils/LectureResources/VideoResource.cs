using System;
using System.Text;

namespace Academy.Models.Utils.LectureResources
{
    public class VideoResource : Resource
    {
        public VideoResource(string name, string url, DateTime uploaded) : base(name, url)
        {
            this.UploadedOn = uploaded;
        }

        public DateTime UploadedOn { get; set; }

        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.AppendLine($"     - Type: Video");
            builder.AppendLine($"     - Uploaded on: {this.UploadedOn}");
            return base.ToString() + builder.ToString().TrimEnd();
        }
    }
}
