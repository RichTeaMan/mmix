using System;
using System.Collections.Generic;
using System.Text;

namespace mmixal
{
    public class RegisterCompilerVariable : IAssemblerVariable
    {
        public int Register { get; }

        public RegisterCompilerVariable(int register)
        {
            Register = register;
        }

        public byte FetchByteReference()
        {
            return (byte)Register;
        }
    }
}
