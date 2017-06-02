using System;
using System.Collections.Generic;

namespace Academy.Models.Contracts
{
    public interface ILecture
    {
        string Name { get; set; }

        DateTime Date { get; set; }

        ITrainer Trainer { get; set; }

        IList<ILectureResource> Resources { get; }
    }
}
