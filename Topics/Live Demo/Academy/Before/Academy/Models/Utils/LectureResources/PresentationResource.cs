using System;
using System.Text;

namespace Academy.Models.Utils.LectureResources
{
    public class PresentationResource : Resource
    {
        public PresentationResource(string name, string url) : base(name, url)
        {
        }

        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.AppendLine($"     - Type: Presentation");
            return base.ToString() + builder.ToString().TrimEnd();
        }
    }
}
