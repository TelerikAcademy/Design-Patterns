using Academy.Core.Contracts;
using Academy.Models;
using System.Collections.Generic;

namespace Academy.Core.Providers
{
    public class AcademyDatabase : IAcademyDatabase
    {
        private readonly IList<Season> seasons;
        private readonly IList<Student> students;
        private readonly IList<Trainer> trainers;

        public AcademyDatabase()
        {
            this.seasons = new List<Season>();
            this.students = new List<Student>();
            this.trainers = new List<Trainer>();
        }

        public IList<Season> Seasons
        {
            get
            {
                return this.seasons;
            }
        }

        public IList<Student> Students
        {
            get
            {
                return this.students;
            }
        }

        public IList<Trainer> Trainers
        {
            get
            {
                return this.trainers;
            }
        }
    }
}