using lib;
using mmix;
using mmixal.Instructions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace mmixal
{
    class Program
    {
        public static string VersionNumber => typeof(Program).Assembly
          .GetCustomAttribute<AssemblyInformationalVersionAttribute>()
          .InformationalVersion;

        static int Main(string[] args)
        {
            Console.WriteLine("MMIXAL Assembler");
            Console.WriteLine($"Thomas Holmes 2020. {VersionNumber}");

            if (args.Length < 1)
            {
                Console.WriteLine("A source file must be specified");
                return -1;
            }
            string objectFile = args[0];

            var asmLines = new List<AsmLine>();
            var operators = ReflectionUtilities.FindExtendingClasses<AbstractInstruction>().ToArray();

            ulong lineNumber = 1;
            var assemblerState = new AssemblerState();
            using (var stream = File.OpenRead(objectFile))
            using (var reader = new StreamReader(stream))
            {
                ulong virtualProgramCounter = 0;
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    if (string.IsNullOrWhiteSpace(line))
                    {
                        continue;
                    }

                    AsmLine asmLine;
                    try
                    {
                        asmLine = AsmLine.Parse(line);
                    }
                    catch (Exception ex)
                    {
                        assemblerState.RaiseError(ex.Message);
                        continue;
                    }                    

                    // hacks to forward read address references.
                    if (asmLine.Op == "LOC" && assemblerState.TryParseConstant(asmLine.Expr, out ulong location))
                    {
                        virtualProgramCounter = location;
                    }
                    if (!string.IsNullOrWhiteSpace(asmLine.Label))
                    {
                        assemblerState.DefineVariable(asmLine.Label, new OctaConstantAssemblerVariable(virtualProgramCounter));
                    }
                    virtualProgramCounter += operators.SingleOrDefault(o => o.SupportsSymbol(asmLine.Op))?.DetermineByteLength(asmLine) ?? 0;

                    asmLines.Add(asmLine);
                    lineNumber++;
                }
            }

            var outFile = objectFile.Replace(".mms", ".mmo");

            if (!assemblerState.Errors.Any())
            {
                using (var outStream = File.OpenWrite(outFile))
                using (var streamWriter = new StreamWriter(outStream))
                {
                    foreach (var asmLine in asmLines)
                    {
                        AbstractInstruction op = operators.SingleOrDefault(o => o.SupportsSymbol(asmLine.Op));
                        if (op is null)
                        {
                            // unknown operation
                            op = new ErroneousInstruction(asmLine.Op);
                        }

                        OperatorOutput output = null;
                        try
                        {
                            output = op.GenerateBinary(assemblerState, asmLine);
                        }
                        catch (Exception ex)
                        {
                            assemblerState.RaiseError(ex.Message);
                        }

                        if (!string.IsNullOrWhiteSpace(output?.Warning))
                        {
                            assemblerState.RaiseWarning(output?.Warning);
                        }
                        if (output.Output != null)
                        {
                            // ensure bytes are multiple of 4
                            var bytes = new List<byte>(output.Output);
                            for (int skip = 0; skip < bytes.Count; skip += 4)
                            {
                                var byteLine = bytes.Skip(skip).Take(4).ToArray();
                                streamWriter.WriteLine($"{assemblerState.ProgramCounter:x}: {byteLine.ToHexString()}");
                                assemblerState.ProgramCounter += 4;
                            }
                        }
                    }
                }
            }

            Console.WriteLine($"Program assembled with {assemblerState.Warnings.Count} warnings and {assemblerState.Errors.Count} errors");
            foreach(var warning in assemblerState.Warnings)
            {
                Console.WriteLine($"Warning - {warning}");
            }
            foreach (var error in assemblerState.Errors)
            {
                Console.WriteLine($"Error - {error}");
            }

            if (assemblerState.Errors.Any())
            {
                Console.WriteLine("Program not written due to previous errors.");
                File.Delete(outFile);
            }
            else
            {
                Console.WriteLine($"Program written to {outFile}.");
            }

            return 0;
        }
    }
}
