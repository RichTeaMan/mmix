using System;
using System.Collections.Generic;

namespace mmixal
{
    public class AssemblerState
    {
        private Dictionary<string, IAssemblerVariable> _definedVariables = new Dictionary<string, IAssemblerVariable>();
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

        public void DefineVariable(string variableName, IAssemblerVariable compilerVariable)
        {
            _definedVariables.Add(variableName, compilerVariable);
        }

    }
}
