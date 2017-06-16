using SchoolSystem.Framework.Models.Contracts;
using SchoolSystem.Framework.Models.Enums;

namespace SchoolSystem.Framework.Core.Contracts
{
    public interface IStudentFactory
    {
        IStudent CreateStudent(string firstName, string lastName, Grade grade);
    }
}