using System;
using System.Collections.Generic;
using System.Text;

namespace mmix.Instructions
{
    public abstract class AbstractInstruction
    {
        public abstract byte OpCode { get; }

        public abstract string Symbol { get; }

        public virtual string SpecificationLink => $"https://mmix.cs.hm.edu/doc/instructions-en.html#{OpCode}";

        public ExecutionResult Execute(MmixComputer mmixComputer, Tetra tetra)
        {
            //Console.WriteLine(Symbol);
            return ExecuteInstruction(mmixComputer, tetra);
        }

        public abstract ExecutionResult ExecuteInstruction(MmixComputer mmixComputer, Tetra tetra);
    }
}
