using Academy.Models;
using Academy.Models.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academy.Core.Contracts
{
    public interface IAcademyDatabase
    {
        IList<Season> Seasons { get; }

        IList<Student> Students { get; }

        IList<Trainer> Trainers { get; }
    }
}
