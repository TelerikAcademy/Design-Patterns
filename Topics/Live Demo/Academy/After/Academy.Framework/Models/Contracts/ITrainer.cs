using System.Collections.Generic;

namespace Academy.Models.Contracts
{
    public interface ITrainer : IUser
    {
        IList<string> Technologies { get; set; }
    }
}
