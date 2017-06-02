using Academy.Models.Contracts;
using Academy.Models.Utils.Contracts;
using System;

namespace Academy.Core.Contracts
{
    public interface IAcademyFactory
    {
        ISeason CreateSeason(string startingYear, string endingYear, string initiative);

        IStudent CreateStudent(string username, string track);

        ITrainer CreateTrainer(string username, string technologies);

        ICourse CreateCourse(string name, string lecturesPerWeek, string startingDate);

        ILecture CreateLecture(string name, string date, ITrainer trainer);

        ILectureResource CreateLectureResource(string type, string name, string url);

        ICourseResult CreateCourseResult(ICourse course, string examPoints, string coursePoints);
    }
}
