using mmixal.PseudoInstructions;
using System;
using System.Collections.Generic;
using System.Text;

namespace mmixal.Instructions
{
    public class BytePseudoInstruction : AbstractPseudoInstruction
    {
        public override string[] SupportedSymbols => new[] { "BYTE" };

        public override OperatorOutput GenerateBinary(AssemblerState assemblerState, AsmLine asmLine)
        {
            List<byte> bytes = new List<byte>();
            string[] args = new[] { asmLine.X, asmLine.Y, asmLine.Z };
            foreach(var arg in args)
            {
                if (TryParseConstant(arg, out byte b))
                {
                    bytes.Add(b);
                }
                else if (arg.StartsWith('"') && arg.EndsWith('"'))
                {
                    bytes.AddRange(Encoding.ASCII.GetBytes(arg.Trim('"')));
                }
                else
                {
                    throw new Exception($"Unable to generate byte string. Unknown argument  format: '{arg}'.");
                }
            }

            return new OperatorOutput() { Output = bytes.ToArray() };
        }
    }
}
