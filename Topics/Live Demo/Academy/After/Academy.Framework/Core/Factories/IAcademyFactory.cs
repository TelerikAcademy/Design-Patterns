using Academy.Models;
using Academy.Models.Contracts;
using Academy.Models.Utils;
using Academy.Models.Utils.Contracts;
using System;

namespace Academy.Core.Contracts
{
    public interface IAcademyFactory
    {
        Season CreateSeason(string startingYear, string endingYear, string initiative);

        Student CreateStudent(string username, string track);

        Trainer CreateTrainer(string username, string technologies);

        Course CreateCourse(string name, string lecturesPerWeek, string startingDate);

        Lecture CreateLecture(string name, string date, ITrainer trainer);

        ILectureResource CreateLectureResource(string type, string name, string url);

        CourseResult CreateCourseResult(ICourse course, string examPoints, string coursePoints);
    }
}
