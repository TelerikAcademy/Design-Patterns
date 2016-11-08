using SchoolSystem.Framework.Models.Contracts;
using SchoolSystem.Framework.Models.Enums;

namespace SchoolSystem.Framework.Core.Contracts
{
    public interface IMarkFactory
    {
        IMark CreateMark(Subject subject, float value);
    }
}