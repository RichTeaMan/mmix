using mmix.Instructions;
using mmixal.PseudoInstructions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace mmixal
{
    public class AssemblyInstruction
    {
        public AsmLine AsmLine { get; }

        public AbstractOperator Op { get; }

        public AssemblyInstruction(AbstractOperator op, AsmLine asmLine)
        {
            AsmLine = asmLine ?? throw new ArgumentNullException(nameof(asmLine));
            Op = op ?? throw new ArgumentNullException(nameof(op));
        }

        private static string fetchArrayElement(string[] tokens, int index)
        {
            string result = string.Empty;
            if (index >= 0 && index < tokens.Length)
            {
                result = tokens[index];
            }
            return result;
        }

        public static AssemblyInstruction ReadFromLine(ICollection<AbstractOperator> operators, ulong lineNumber, string line)
        {
            if (operators is null)
            {
                throw new ArgumentNullException(nameof(operators));
            }
            
            var asmLine = AsmLine.Parse(line);

            AbstractOperator op = operators.SingleOrDefault(o => o.SupportsSymbol(asmLine.Op));
            if (op is null)
            {
                // unknown operation
                op = new ErroneousOperator(asmLine.Op);
            }
            return new AssemblyInstruction(op, asmLine);
        }

        public void GenerateOutput(AssemblerState assemblerState, StreamWriter streamWriter)
        {
            var output = Op.GenerateBinary(assemblerState, AsmLine);

            if (!string.IsNullOrWhiteSpace(output.Warning))
            {
                assemblerState.RaiseWarning(output.Warning);
            }
            if (!(Op is AbstractPseudoInstruction))
            {
                streamWriter.WriteLine($"#{assemblerState.ProgramCounter.ToString("x")}: {output.Output}");
                assemblerState.ProgramCounter += 4;
            }
        }
    }
}
