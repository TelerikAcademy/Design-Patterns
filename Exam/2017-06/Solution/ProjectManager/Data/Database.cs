using System.Collections.Generic;

using ProjectManager.Framework.Data.Models;

namespace ProjectManager.Framework.Data
{
    public class Database : IDatabase
    {
        private readonly IList<Project> projects;

        public Database()
        {
            this.projects = new List<Project>();
        }

        public IList<Project> Projects
        {
            get
            {
                return projects;
            }
        }
    }
}
