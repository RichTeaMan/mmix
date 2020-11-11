using System;
using System.Collections.Generic;
using System.Text;

namespace mmixal.PseudoInstructions
{
    public abstract class AbstractPseudoInstruction : AbstractOperator
    {
        public override ulong DetermineByteLength(AsmLine asmLine) => 0;
    }
}
