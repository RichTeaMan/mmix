using mmixal.PseudoInstructions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace mmixal
{
    class Program
    {
        static int Main(string[] args)
        {
            Console.WriteLine("MMIXAL Assembler");


            if (args.Length < 1)
            {
                Console.WriteLine("A source file must be specified");
                return -1;
            }
            string objectFile = args[0];
            var bytes = Encoding.ASCII.GetBytes(objectFile.Replace(".mmo", string.Empty));


            var instructions = new List<AssemblyInstruction>();
            var operators = new List<AbstractOperator>();
            operators.Add(new IsInstruction());
            operators.Add(new LocInstruction());

            ulong lineNumber = 1;
            using (var stream = File.OpenRead(objectFile))
            using (var reader = new StreamReader(stream))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    var instruction = AssemblyInstruction.ReadFromLine(operators, lineNumber, line);
                    instructions.Add(instruction);
                    lineNumber++;
                }
            }

            var log = new AssemblerState();
            var outFile = objectFile.Replace(".mms", ".mmo");
            File.Delete(outFile);
            using (var outStream = File.OpenWrite(outFile))
            using (var streamWriter = new StreamWriter(outStream))
            {
                foreach (var instruction in instructions)
                {
                    instruction.GenerateOutput(log, streamWriter);
                }
            }

            return 0;
        }
    }
}
