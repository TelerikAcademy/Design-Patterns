using System.Collections.Generic;

using ProjectManager.Framework.Data.Models;

namespace ProjectManager.Framework.Data
{
    public interface IDatabase
    {
        IList<Project> Projects { get; }
    }
}
