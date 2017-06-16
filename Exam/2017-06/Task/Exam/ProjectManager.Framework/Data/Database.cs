using System.Collections.Generic;
using System;
using ProjectManager.Framework.Data;
using ProjectManager.Framework.Data.Models;

namespace ProjectManager.Data
{
    public class Database : IDatabase
    {
        private IList<Project> projects;

        private static Database instance;

        private Database()
        {
            this.projects = new List<Project>();
        }

        // Singleton design pattern
        public static Database Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Database();
                }

                return instance;
            }
        }

        public IList<Project> Projects
        {
            get
            {
                return this.projects;
            }
        }
    }
}
