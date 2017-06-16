using System.Collections.Generic;

using ProjectManager.Framework.Data.Models;

namespace ProjectManager.Framework.Data
{
    // You are not allowed to modify this interface (not even to remove this comment)
    public interface IDatabase
    {
        IList<Project> Projects { get; }
    }
}
