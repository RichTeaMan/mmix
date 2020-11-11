using lib;
using mmix;
using mmix.Instructions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mmixal.PseudoInstructions
{
    public class ThreeArgsInstruction : AbstractOperator
    {
        private AbstractInstruction[] instructions = null;

        public override string[] SupportedSymbols => new[] { "LDOU", "TRAP" };

        public override OperatorOutput GenerateBinary(AssemblerState assemblerState, AsmLine asmLine)
        {
            if (instructions == null)
            {
                instructions = ReflectionUtilities.FindExtendingClasses<AbstractInstruction>().ToArray();
            }

            var instruction = instructions.SingleOrDefault(i => i.Symbol == asmLine.Op);
            if (instruction == null)
            {
                throw new Exception("Unknown OP code.");
            }
            var hex = new byte[] {
                instruction.OpCode,
                assemblerState.ParseExprToken(asmLine.X).FetchByte(),
                assemblerState.ParseExprToken(asmLine.Y).FetchByte(),
                assemblerState.ParseExprToken(asmLine.Z).FetchByte(),
            };
            return new OperatorOutput() { Output = hex };
        }
    }
}
