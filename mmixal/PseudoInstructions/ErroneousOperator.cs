using System;
using System.Collections.Generic;
using System.Text;

namespace mmixal.PseudoInstructions
{
    public class ErroneousOperator : AbstractOperator
    {


        public override string Symbol { get; }
        public ErroneousOperator(string opcode)
        {
            Symbol = opcode;
        }

        public override OperatorOutput GenerateBinary(string expression)
        {
            return new OperatorOutput
            {
                Warning = $"'{Symbol}' is not a thing."
            };
        }
    }
}
