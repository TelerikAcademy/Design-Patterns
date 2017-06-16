using SchoolSystem.Framework.Models.Enums;

namespace SchoolSystem.Framework.Models.Contracts
{
    /// <summary>
    /// Represents the Mark Students get from Teachers during class, that contains the Mark's subject and value.
    /// </summary>
    public interface IMark
    {
        Subject Subject { get; }

        float Value { get; }
    }
}
