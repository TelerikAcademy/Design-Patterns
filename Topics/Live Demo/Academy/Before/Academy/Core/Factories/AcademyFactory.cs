using Academy.Core.Contracts;
using Academy.Core.Providers;
using Academy.Models;
using Academy.Models.Contracts;
using Academy.Models.Enums;
using Academy.Models.Utils;
using Academy.Models.Utils.Contracts;
using Academy.Models.Utils.LectureResources;
using System;

namespace Academy.Core.Factories
{
    public class AcademyFactory : IAcademyFactory
    {
        private static IAcademyFactory instanceHolder = new AcademyFactory();

        // private because of Singleton design pattern
        private AcademyFactory()
        {
        }

        public static IAcademyFactory Instance
        {
            get
            {
                return instanceHolder;
            }
        }

        public ISeason CreateSeason(string startingYear, string endingYear, string initiative)
        {
            var parsedStartingYear = int.Parse(startingYear);
            var parsedEngingYear = int.Parse(endingYear);

            Initiative parsedInitiativeAsEnum;
            Enum.TryParse<Initiative>(initiative, out parsedInitiativeAsEnum);

            return new Season(parsedStartingYear, parsedEngingYear, parsedInitiativeAsEnum);
        }

        public IStudent CreateStudent(string username, string track)
        {
            Track parsedTrackAsEnum;
            Enum.TryParse<Track>(track, out parsedTrackAsEnum);

            return new Student(username, parsedTrackAsEnum);
        }

        public ITrainer CreateTrainer(string username, string technologies)
        {            
            return new Trainer(username, technologies.Split(','));
        }

        public ICourse CreateCourse(string name, string lecturesPerWeek, string startingDate)
        {
            var lectures = int.Parse(lecturesPerWeek);
            var starting = DateTime.Parse(startingDate);
            var ending = starting.AddDays(30);

            return new Course(name, lectures, starting, ending);
        }

        public ILecture CreateLecture(string name, string date, ITrainer trainer)
        {
            var parsedDate = DateTime.Parse(date);

            return new Lecture(name, parsedDate, trainer);
        }

        public ILectureResource CreateLectureResource(string type, string name, string url)
        {
            // Use this instead of DateTime.Now if you want any points in BGCoder!!
            var currentDate = DateTimeProvider.Now;

            switch (type)
            {
                case "video": return new VideoResource(name, url, currentDate);
                case "presentation": return new PresentationResource(name, url);
                case "demo": return new DemoResource(name, url);
                case "homework": return new HomeworkResource(name, url, currentDate.AddDays(7));
                default: throw new ArgumentException("Invalid lecture resource type");
            }
        }

        public ICourseResult CreateCourseResult(ICourse course, string examPoints, string coursePoints)
        {
            return new CourseResult(course, float.Parse(examPoints), float.Parse(coursePoints));
        }
    }
}
