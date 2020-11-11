using System;
using System.Collections.Generic;
using System.Text;

namespace mmixal.Instructions
{
    public class BytePseudoInstruction : AbstractPseudoInstruction
    {
        public override string[] SupportedSymbols => new[] { "BYTE" };

        public override ulong DetermineByteLength(AsmLine asmLine)
        {
            List<byte> bytes = new List<byte>();
            string[] args = new[] { asmLine.X, asmLine.Y, asmLine.Z };
            foreach (var arg in asmLine.Args)
            {
                // TODO multibyte constants
                if (TryParseConstant(arg, out byte b))
                {
                    bytes.Add(b);
                }
                else if (arg.StartsWith('"') && arg.EndsWith('"'))
                {
                    bytes.AddRange(Encoding.ASCII.GetBytes(arg.Trim('"')));
                }
            }

            return (ulong)bytes.Count;
        }

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
                else if (string.IsNullOrEmpty(arg))
                {
                    // do nothing
                }
                else
                {
                    throw new Exception($"Unable to generate byte string. Unknown argument  format: '{arg}'.");
                }
            }

            if (!string.IsNullOrEmpty(asmLine.Label))
            {
                assemblerState.DefineVariable(asmLine.Label, new OctaConstantAssemblerVariable(assemblerState.ProgramCounter));
            }

            return new OperatorOutput() { Output = bytes.ToArray() };
        }
    }
}
