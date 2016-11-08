namespace SchoolSystem.Framework.Models.Contracts
{
    public interface IGetTeacher
    {
        ITeacher GetTeacher(int teacherId);
    }
}