using ProjectManager.Framework.Data.Models;

namespace ProjectManager.Framework.Data.Factories
{
    public interface IModelsFactory
    {
        Project CreateProject(string name, string startingDate, string endingDate, string state);

        Task CreateTask(User owner, string name, string state);

        User CreateUser(string username, string email);
    }
}
