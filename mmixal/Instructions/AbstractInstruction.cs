using System;
using System.Globalization;
using System.Linq;

namespace mmixal.Instructions
{
    public abstract class AbstractInstruction
    {
        public abstract string[] SupportedSymbols { get; }

        public bool SupportsSymbol(string symbol)
        {
            return SupportedSymbols.Contains(symbol);
        }

        public bool TryParseRegister(string expression, out byte registerValue)
        {
            registerValue = 0;
            return !string.IsNullOrWhiteSpace(expression) &&
                expression.StartsWith("$") &&
                byte.TryParse(expression.Remove(0, 1), out registerValue);
        }

        public bool TryParseConstant(string expression, out ulong value)
        {
            value = 0;
            return !string.IsNullOrWhiteSpace(expression) &&
                ((expression.StartsWith("#") &&
                ulong.TryParse(expression.Remove(0, 1), NumberStyles.HexNumber, null, out value)) ||
                ulong.TryParse(expression, out value));
        }

        public bool TryParseConstant(string expression, out byte value)
        {
            value = 0;
            return !string.IsNullOrWhiteSpace(expression) &&
                ((expression.StartsWith("#") &&
                byte.TryParse(expression.Remove(0, 1), NumberStyles.HexNumber, null, out value)) ||
                byte.TryParse(expression, out value));
        }

        public abstract OperatorOutput GenerateBinary(AssemblerState assemblerState, AsmLine asmLine);

        /// <summary>
        /// Gets the length in bytes this operation will take in memory.
        /// </summary>
        public virtual ulong DetermineByteLength(AsmLine asmLine) => 4;
    }
}
