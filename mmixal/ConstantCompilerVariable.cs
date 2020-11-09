using System;
using System.Collections.Generic;
using System.Text;

namespace mmixal
{
    public class ConstantCompilerVariable : ICompilerVariable
    {
        public int Constant { get; }

        public ConstantCompilerVariable(int constant)
        {
            Constant = constant;
        }
    }
}
