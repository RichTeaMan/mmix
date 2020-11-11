using System;
using System.Collections.Generic;
using System.Globalization;

namespace mmixal
{
    public class AssemblerState
    {
        private Dictionary<string, IAssemblerVariable> _definedVariables = new Dictionary<string, IAssemblerVariable>()
        {
            { "Halt", (ConstantAssemblerVariable)0 },

            { "Fopen",  (ConstantAssemblerVariable)1 },
            { "Fclose", (ConstantAssemblerVariable)2 },
            { "Fread",  (ConstantAssemblerVariable)3 },
            { "Fgets",  (ConstantAssemblerVariable)4 },
            { "Fgetws", (ConstantAssemblerVariable)5 },
            { "Fwrite", (ConstantAssemblerVariable)6 },
            { "Fputs",  (ConstantAssemblerVariable)7 },
            { "Fputws", (ConstantAssemblerVariable)8 },
            { "Fseek",  (ConstantAssemblerVariable)9 },
            { "Ftell",  (ConstantAssemblerVariable)10 },

            { "TextRead",    (ConstantAssemblerVariable)0 },
            { "TextWrite",   (ConstantAssemblerVariable)1 },
            { "BinaryRead",  (ConstantAssemblerVariable)2 },
            { "BinaryWrite", (ConstantAssemblerVariable)3 },
            { "BinaryReadWrite", (ConstantAssemblerVariable)4 },

            { "StdIn",  (ConstantAssemblerVariable)0 },
            { "StdOut", (ConstantAssemblerVariable)1 },
            { "StdErr", (ConstantAssemblerVariable)2 },
        };

        public IReadOnlyDictionary<string, IAssemblerVariable> DefinedVariables { get { return _definedVariables; } }

        public ulong ProgramCounter { get; set; } = 0;

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
        public byte ParseExprToken(string token)
        {
            if (_definedVariables.TryGetValue(token, out IAssemblerVariable assemblerVariable))
            {
                return assemblerVariable.FetchByteReference();
            }
            if (TryParseRegister(token, out byte register))
            {
                return register;
            }
            if (TryParseConstant(token, out ulong constant))
            {
                return (byte)constant;
            }
            throw new Exception($"'{token}' cannot be resolved.");
        }

        public void DefineVariable(string variableName, IAssemblerVariable compilerVariable)
        {
            _definedVariables.Add(variableName, compilerVariable);
        }

    }
}
