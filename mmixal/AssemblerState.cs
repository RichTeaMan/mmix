using System;
using System.Collections.Generic;

namespace mmixal
{
    public class AssemblerState
    {
        private Dictionary<string, ICompilerVariable> _definedVariables = new Dictionary<string, ICompilerVariable>();
        public IReadOnlyDictionary<string, ICompilerVariable> DefinedVariables { get { return _definedVariables; } }

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

        public void DefineVariable(string variableName, ICompilerVariable compilerVariable)
        {
            _definedVariables.Add(variableName, compilerVariable);
        }

    }
}
