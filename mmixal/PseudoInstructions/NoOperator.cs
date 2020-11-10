using System;
using System.Collections.Generic;
using System.Text;

namespace mmixal.PseudoInstructions
{
    public class NoOperator : AbstractOperator
    {
        public override string[] SupportedSymbols => new[] { string.Empty };
        public override OperatorOutput GenerateBinary(AssemblerState assemblerState, AsmLine asmLine)
        {
            return new OperatorOutput();
        }
    }
}
