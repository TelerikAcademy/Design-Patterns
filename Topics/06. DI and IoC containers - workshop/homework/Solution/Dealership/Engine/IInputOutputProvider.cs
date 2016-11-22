using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dealership.Engine
{
    public interface IInputOutputProvider
    {
        string Read();

        void Write(string output);
    }
}