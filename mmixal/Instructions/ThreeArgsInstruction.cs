using lib;
using System;
using System.Linq;

namespace mmixal.Instructions
{
    public class ThreeArgsInstruction : AbstractInstruction
    {
        private mmix.Instructions.AbstractInstruction[] instructions = null;

        public override string[] SupportedSymbols => new[] { "LDOU", "TRAP" };

        public override OperatorOutput GenerateBinary(AssemblerState assemblerState, AsmLine asmLine)
        {
            if (instructions == null)
            {
                instructions = ReflectionUtilities.FindExtendingClasses<mmix.Instructions.AbstractInstruction>().ToArray();
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
