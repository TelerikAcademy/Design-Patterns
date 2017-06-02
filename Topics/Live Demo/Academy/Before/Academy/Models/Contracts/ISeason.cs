using Academy.Models.Enums;
using System;
using System.Collections.Generic;

namespace Academy.Models.Contracts
{
    public interface ISeason
    {
        Initiative Initiative { get; set; }

        int StartingYear { get; set; }

        int EndingYear { get; set; }

        IList<IStudent> Students { get; set; }

        IList<ITrainer> Trainers { get; set; }

        IList<ICourse> Courses { get; set; }

        string ListUsers();

        string ListCourses();
    }
}
