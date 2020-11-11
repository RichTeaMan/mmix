using lib;
using mmixal.PseudoInstructions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

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

            var instructions = new List<AssemblyInstruction>();
            var operators = ReflectionUtilities.FindExtendingClasses<AbstractOperator>().ToArray();

            ulong lineNumber = 1;
            var assemblerState = new AssemblerState();
            using (var stream = File.OpenRead(objectFile))
            using (var reader = new StreamReader(stream))
            {
                ulong virtualProgramCounter = 0;
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    var instruction = AssemblyInstruction.ReadFromLine(operators, lineNumber, line);

                    // hacks to forward read address references.
                    if (instruction.Op is LocInstruction && assemblerState.TryParseConstant(instruction.AsmLine.Expr, out ulong location))
                    {
                        virtualProgramCounter = location;
                    }
                    if (!string.IsNullOrWhiteSpace(instruction.AsmLine.Label))
                    {
                        assemblerState.DefineVariable(instruction.AsmLine.Label, new OctaConstantAssemblerVariable(virtualProgramCounter));
                    }
                    virtualProgramCounter += instruction.Op.DetermineByteLength(instruction.AsmLine);

                    instructions.Add(instruction);
                    lineNumber++;
                }
            }

            var outFile = objectFile.Replace(".mms", ".mmo");
            File.Delete(outFile);
            using (var outStream = File.OpenWrite(outFile))
            using (var streamWriter = new StreamWriter(outStream))
            {
                foreach (var instruction in instructions)
                {
                    instruction.GenerateOutput(assemblerState, streamWriter);
                }
            }

            Console.WriteLine($"Written to {outFile}.");

            return 0;
        }
    }
}
