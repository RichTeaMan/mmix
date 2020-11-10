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
            // TODO what if constant is larger than a byte?
            return (byte)Constant;
        }

        public static implicit operator ConstantAssemblerVariable(int c) => new ConstantAssemblerVariable(c);
    }
}
