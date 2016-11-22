using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dealership.Engine
{
    public class ConsoleInputOutputProvider : IInputOutputProvider
    {
        public string Read()
        {
            return Console.ReadLine();
        }

        public void Write(string output)
        {
            Console.Write(output);
        }
    }
}