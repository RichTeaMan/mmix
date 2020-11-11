using mmix;
using System;
using System.Linq;

namespace mmixal.Instructions
{
    public class GetAInstruction : AbstractInstruction
    {

        public override string[] SupportedSymbols => new[] { "GETA" };

        public override OperatorOutput GenerateBinary(AssemblerState assemblerState, AsmLine asmLine)
        {
            if (asmLine.ArgCount != 2)
            {
                throw new Exception("GETA must have exactly two arguments.");
            }

            // TODO don't forget GETAB[ackwards]
            bool backward = false;

            var destinationToken = assemblerState.ParseExprToken(asmLine.X);
            if (destinationToken.TokenType != ExprToken.ExprTokenType.REGISTER)
            {
                throw new Exception($"GETA X argument must be a register. {destinationToken.TokenType} received.");
            }

            ushort relativePointer;
            var pointerToken = assemblerState.ParseExprToken(asmLine.Y);
            if (pointerToken.TokenType == ExprToken.ExprTokenType.VARIABLE)
            {
                var octaVar = pointerToken.AssemblerVariable as OctaConstantAssemblerVariable;
                if (octaVar == null)
                {
                    throw new Exception($"GETA reference must be an OCTA.");
                }
                // calculate relative pointer
                // TODO a different instruction is emitted if pointer points backwards. Care must be taken with unsigned weirdness.
                // divide by 4 for tetra alignment.
                relativePointer = (ushort)((octaVar.Constant - assemblerState.ProgramCounter).ToULong() / 4);
            }
            else if (pointerToken.TokenType == ExprToken.ExprTokenType.CONSTANT)
            {
                relativePointer = pointerToken.Value;
            }
            else
            {
                throw new Exception("GETA RA must be an OCTA reference or a constant.");
            }

            // GETA code.
            byte opCode = 0xF4;
            if (backward)
            {
                // GETAB code.
                opCode = 0xF5;
            }


            var hex = new byte[] {
                opCode,
                destinationToken.Value }
            .Concat(relativePointer.ToBytes())
            .ToArray();
            
            return new OperatorOutput() { Output = hex };
        }
    }
}
