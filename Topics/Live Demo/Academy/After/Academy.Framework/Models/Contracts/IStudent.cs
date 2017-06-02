using Academy.Models.Enums;
using Academy.Models.Utils.Contracts;
using System.Collections.Generic;

namespace Academy.Models.Contracts
{
    public interface IStudent : IUser
    {
        Track Track { get; set; }

        IList<ICourseResult> CourseResults { get; set; }
    }
}
