using mmix;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace mmixal
{
    public class AssemblerState
    {
        private Dictionary<string, IAssemblerVariable> _definedVariables = new Dictionary<string, IAssemblerVariable>()
        {
            { "Halt", (ByteConstantAssemblerVariable)0 },

            { "Fopen",  (ByteConstantAssemblerVariable)1 },
            { "Fclose", (ByteConstantAssemblerVariable)2 },
            { "Fread",  (ByteConstantAssemblerVariable)3 },
            { "Fgets",  (ByteConstantAssemblerVariable)4 },
            { "Fgetws", (ByteConstantAssemblerVariable)5 },
            { "Fwrite", (ByteConstantAssemblerVariable)6 },
            { "Fputs",  (ByteConstantAssemblerVariable)7 },
            { "Fputws", (ByteConstantAssemblerVariable)8 },
            { "Fseek",  (ByteConstantAssemblerVariable)9 },
            { "Ftell",  (ByteConstantAssemblerVariable)10 },

            { "TextRead",    (ByteConstantAssemblerVariable)0 },
            { "TextWrite",   (ByteConstantAssemblerVariable)1 },
            { "BinaryRead",  (ByteConstantAssemblerVariable)2 },
            { "BinaryWrite", (ByteConstantAssemblerVariable)3 },
            { "BinaryReadWrite", (ByteConstantAssemblerVariable)4 },

            { "StdIn",  (ByteConstantAssemblerVariable)0 },
            { "StdOut", (ByteConstantAssemblerVariable)1 },
            { "StdErr", (ByteConstantAssemblerVariable)2 },
        };

        public IReadOnlyDictionary<string, IAssemblerVariable> DefinedVariables { get { return _definedVariables; } }

        public Octa ProgramCounter { get; set; } = 0;

        public AssemblerState RaiseWarning(string message)
        {
            Console.WriteLine($"Warning - {message}");
            return this;
        }

        public AssemblerState RaiseError(string message)
        {
            Console.WriteLine($"Warning - {message}");
            return this;
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

        /// <summary>
        /// Attempts to parse a token. The token should be an expression (not an op code) and can resolve a raw constant, a register reference, or an alias.
        /// </summary>
        /// <returns></returns>
        public ExprToken ParseExprToken(string token)
        {
            if (_definedVariables.TryGetValue(token, out IAssemblerVariable assemblerVariable))
            {
                return new ExprToken(assemblerVariable);
            }
            if (TryParseRegister(token, out byte register))
            {
                return new ExprToken(ExprToken.ExprTokenType.REGISTER, register);
            }
            if (TryParseConstant(token, out ulong constant))
            {
                // TODO byte casting is probably not the play
                return new ExprToken(ExprToken.ExprTokenType.CONSTANT, (byte)constant);
            }
            throw new Exception($"'{token}' cannot be resolved.");
        }

        public void DefineVariable(string variableName, IAssemblerVariable compilerVariable)
        {
            _definedVariables.Remove(variableName);
            _definedVariables.Add(variableName, compilerVariable);
        }

    }
}
