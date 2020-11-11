using mmix;
using System;
using System.Collections.Generic;
using System.Text;

namespace mmixal
{
    public class OctaConstantAssemblerVariable : IAssemblerVariable
    {
        public Octa Constant { get; }

        public OctaConstantAssemblerVariable(Octa constant)
        {
            Constant = constant;
        }

        public byte FetchByteReference()
        {
            // TODO idk, I didn't think this interface through.
            throw new NotImplementedException();
        }

        public static implicit operator OctaConstantAssemblerVariable(int c) => new OctaConstantAssemblerVariable(c);
    }
}
