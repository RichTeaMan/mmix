using System;
using System.Collections.Generic;
using System.Text;

namespace mmixal
{
    public class ByteConstantAssemblerVariable : IAssemblerVariable
    {
        public byte Constant { get; }

        public ByteConstantAssemblerVariable(byte constant)
        {
            Constant = constant;
        }

        public byte FetchByteReference()
        {
            // TODO what if constant is larger than a byte?
            return (byte)Constant;
        }

        public static implicit operator ByteConstantAssemblerVariable(int c) => new ByteConstantAssemblerVariable((byte)c);
    }
}
