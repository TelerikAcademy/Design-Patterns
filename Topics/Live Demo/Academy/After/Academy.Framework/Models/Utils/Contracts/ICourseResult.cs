using Academy.Models.Contracts;
using Academy.Models.Enums;

namespace Academy.Models.Utils.Contracts
{
    public interface ICourseResult
    {
        ICourse Course { get; }

        float ExamPoints { get; }

        float CoursePoints { get; }

        Grade Grade { get; }
    }
}
