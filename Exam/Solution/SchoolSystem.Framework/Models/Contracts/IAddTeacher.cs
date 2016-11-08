namespace SchoolSystem.Framework.Models.Contracts
{
    public interface IAddTeacher
    {
        void AddTeacher(int teacherId, ITeacher teacher);
    }
}