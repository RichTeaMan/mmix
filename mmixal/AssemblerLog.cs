using System;
using System.Collections.Generic;
using System.Text;

namespace mmixal
{
    public class AssemblerLog
    {
        public AssemblerLog RaiseWarning(string message)
        {
            Console.WriteLine($"Warning - {message}");
            return this;
        }

        public AssemblerLog RaiseError(string message)
        {
            Console.WriteLine($"Warning - {message}");
            return this;
        }
    }
}
