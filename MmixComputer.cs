using mmix.Instructions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace mmix
{
    public class MmixComputer
    {
        private AbstractInstruction[] instructionSet = { new TrapInstruction(), new GetAInstruction(), new LdouInstruction() };
        public Octa[] Registers { get; private set; } = new Octa[256];

        public Octa[] SpecialRegisters { get; private set; } = new Octa[32];

        /// <summary>
        /// Todo: MMIX spec defines 2^64 bytes of memory.
        /// </summary>
        public byte[] Memory { get; } = new byte[1000];

        /// <summary>
        /// Program counter. Also known as @.
        /// </summary>
        public ulong PC { get; set; }

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
            AddToMemory((ulong)address, value);
        }

        public void AddToMemory(int address, ulong value)
        {
            AddToMemory((ulong)address, value);
        }

        public void AddToMemory(ulong address, uint value)
        {
            byte[] intBytes = value.ToBytes();
            foreach (var b in intBytes)
            {
                Memory[address] = b;
                address++;
            }
        }

        public void AddToMemory(ulong address, ulong value)
        {
            byte[] intBytes = value.ToBytes();
            foreach (var b in intBytes)
            {
                Memory[address] = b;
                address++;
            }
        }

        public byte[] ReadMemory(ulong address, int count)
        {
            byte[] bytes = new byte[count];
            for (int i = 0; i < count; i++)
            {
                ulong offset = (ulong)i + address;
                bytes[i] = Memory[offset];
            }
            return bytes;
        }

        public void StoreInMemory(ulong address, Octa octa)
        {
            AddToMemory(AlignAddress(address, 8), octa.ToULong());
        }

        private ulong AlignAddress(ulong address, int alignment)
        {
            return address - (address % (ulong)alignment);
        }

        public Octa ReadOcta(ulong address)
        {
            var bytes = ReadMemory(AlignAddress(address, 8), 8);
            return new Octa(bytes);

        }

        public void LoadStreamIntoMemory(Stream stream)
        {
            using var reader = new StreamReader(stream);
            string line;

            // program counter
            ulong? pc = null;
            // Read and display lines from the file until the end of the file is reached.
            while ((line = reader.ReadLine()) != null)
            {
                string cleanLine = line.Replace(" ", string.Empty).Replace("#", string.Empty);
                var parts = cleanLine.Split(":");
                if (parts.Length != 2)
                {
                    throw new Exception($"Invalid line: {line}");
                }
                string addressStr = parts[0];
                string valueStr = parts[1];

                ulong address;
                uint value;

                if (!(ulong.TryParse(addressStr, System.Globalization.NumberStyles.HexNumber, null, out address) &&
                uint.TryParse(valueStr, System.Globalization.NumberStyles.HexNumber, null, out value)))
                {
                    throw new Exception($"Could not parse hex in line: {line}");
                }

                if (pc == null)
                {
                    pc = address;
                }
                AddToMemory(address, value);
            }
            if (pc == null)
            {
                throw new Exception("Program unable to load.");
            }
            PC = pc.Value;
        }

        public MmixComputer()
        {
            for (int i = 0; i < Registers.Length; i++)
            {
                Registers[i] = new Octa();
            }
        }

        public ExecutionResult Execute()
        {
            var current = new Tetra(new byte[] { Memory[PC], Memory[PC + 1], Memory[PC + 2], Memory[PC + 3] });
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
