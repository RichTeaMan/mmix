using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace mmixal.PseudoInstructions
{
    public abstract class AbstractOperator
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
                expression.StartsWith("#") &&
                ulong.TryParse(expression.Remove(0, 1), NumberStyles.HexNumber, null, out value);
        }

        public abstract OperatorOutput GenerateBinary(AssemblerState assemblerState, AsmLine asmLine);
    }
}
