using System;
using System.Collections.Generic;
using System.Text;

namespace mmixal
{
    public class ConstantAssemblerVariable : IAssemblerVariable
    {
        public int Constant { get; }

        public ConstantAssemblerVariable(int constant)
        {
            Constant = constant;
        }

        public byte FetchByteReference()
        {
            throw new NotImplementedException();
        }
    }
}
