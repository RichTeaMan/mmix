using System;
using System.Collections.Generic;
using System.Text;
using mmixal.Variable;

namespace mmixal
{
    public class AssemblerState
    {
        private Dictionary<string, AbstractVariable> variables = new Dictionary<string, AbstractVariable>();

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

        public AssemblerState CreateRegisterAlias(string variableName, byte value)
        {
            var registerAlias = new RegisterAlias(variableName, value);
            variables.Add(variableName, registerAlias);
            return this;
        }

    }
}
