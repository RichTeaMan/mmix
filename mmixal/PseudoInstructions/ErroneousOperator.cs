using System;
using System.Collections.Generic;
using System.Text;

namespace mmixal.PseudoInstructions
{
    public class ErroneousOperator : AbstractOperator
    {
        public string Symbol { get; }

        public override string[] SupportedSymbols => new string[0];
        public ErroneousOperator(string opcode)
        {
            Symbol = opcode;
        }
        public override OperatorOutput GenerateBinary(AssemblerState assemblerState, AsmLine asmLine)
        {
            return new OperatorOutput
            {
                Warning = $"'{Symbol}' is not a thing."
            };
        }
    }
}
