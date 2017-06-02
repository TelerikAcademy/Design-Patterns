using System;
using System.Collections.Generic;

namespace Academy.Models.Contracts
{
    public interface ICourse
    {
        string Name { get; set; }

        int LecturesPerWeek { get; set; }

        DateTime StartingDate { get; set; }

        DateTime EndingDate { get; set; }

        IList<IStudent> OnsiteStudents { get; }

        IList<IStudent> OnlineStudents { get; }

        IList<ILecture> Lectures { get; }
    }
}
