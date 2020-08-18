using mmix.Instructions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mmix
{
    public class MmixComputer
    {
        private AbstractInstruction[] instructionSet = {new TrapInstruction(), new GetAInstruction(), new LdouInstruction()};
        public Octa[] Registers { get; private set; } = new Octa[256];

        public Octa[] SpecialRegisters { get; private set; } = new Octa[32];

        /// <summary>
        /// Todo: MMIX spec defines 2^64 bytes of memory.
        /// </summary>
        public byte[] Memory { get; } = new byte[1000];

        /// <summary>
        /// Program counter. Also known as @.
        /// </summary>
        public int PC { get; set; }

        /// <summary>
        /// Gets bootstrap register (trap).
        /// </summary>
        public Octa rBB
        {
            get { return SpecialRegisters[26]; }
            set { SpecialRegisters[26] = value; }
        }

        public void AddToMemory(int address, uint value)
        {
            byte[] intBytes = BitConverter.GetBytes(value).Reverse().ToArray();
            foreach(var b in intBytes)
            {
                Memory[address] = b;
                address++;
            }
        }

        public void AddToMemory(int address, ulong value)
        {
            byte[] intBytes = BitConverter.GetBytes(value).Reverse().ToArray();
            foreach (var b in intBytes)
            {
                Memory[address] = b;
                address++;
            }
        }

        public byte[] ReadMemory(int address, int count)
        {
            byte[] bytes = new byte[count];
            for(int i = 0; i < count; i++)
            {
                int offset = i + address;
                bytes[i] = Memory[offset];
            }
            return bytes;
        }

        public MmixComputer()
        {
            for(int i = 0; i < Registers.Length; i++)
            {
                Registers[i] = new Octa();
            }
        }

        public ExecutionResult Execute()
        {
            var current = new Tetra(new byte[] { Memory[PC], Memory[PC+1], Memory[PC+2], Memory[PC+3] });
            var instruction = instructionSet.SingleOrDefault(i => i.OpCode == current.OpCode);
            if (instruction == null)
            {
                throw new Exception("Unknown instruction");
            }
            ExecutionResult executionResult = instruction.Execute(this, current); ;
            PC += 4;
            return executionResult;
        }
    }
}
