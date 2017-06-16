using SchoolSystem.Framework.Core.Commands.Contracts;
using System;

namespace SchoolSystem.Framework.Core.Contracts
{
    public interface ICommandFactory
    {
        ICommand GetCommand(Type type);
    }
}