using System;
using System.Collections.Generic;
using System.Text;

namespace mmixal
{
    public class RegisterCompilerVariable : ICompilerVariable
    {
        public int Register { get; }

        public RegisterCompilerVariable(int register)
        {
            Register = register;
        }
    }
}
