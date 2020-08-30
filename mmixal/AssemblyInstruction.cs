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
        public string Label { get; }

        public AbstractOperator Op { get; }

        public string Expression { get; }

        public AssemblyInstruction(string label, AbstractOperator op, string expression)
        {
            Label = label ?? throw new ArgumentNullException(nameof(label));
            Op = op ?? throw new ArgumentNullException(nameof(op));
            Expression = expression ?? throw new ArgumentNullException(nameof(expression));
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

            // tokenise line by spaces
            var tokens = line?.Split(" ");

            if (tokens.Length < 2)
            {
                return new AssemblyInstruction(string.Empty, new NoOperator(), string.Empty);
            }

            string label = string.Empty;
            AbstractOperator op;
            string expression = string.Empty;

            // OP may be first or second token so check both for valid ops. If it first then label is "".
            var firstOp = operators.SingleOrDefault(o => o.Symbol == tokens[0]);
            var secondOp = operators.SingleOrDefault(o => o.Symbol == tokens[1]);

            // favour second over first op
            if (secondOp != null)
            {
                op = secondOp;
                label = tokens[0];
                expression = fetchArrayElement(tokens, 2);
            }
            else if (firstOp != null)
            {
                op = firstOp;
                expression = fetchArrayElement(tokens, 1);
            }
            else
            {
                // unknown operation
                op = new ErroneousOperator(tokens[0]);
            }
            return new AssemblyInstruction(label, op, expression);
        }

        public void GenerateOutput(AssemblerState assemblerState, StreamWriter streamWriter)
        {
            var output = Op.GenerateBinary(this, assemblerState);

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
