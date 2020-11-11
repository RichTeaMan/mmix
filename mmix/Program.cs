using System;
using System.IO;
using System.Reflection;
using System.Text;

namespace mmix
{
    class Program
    {
        public static string VersionNumber => typeof(Program).Assembly
          .GetCustomAttribute<AssemblyInformationalVersionAttribute>()
          .InformationalVersion;

        static int Main(string[] args)
        {
            Console.WriteLine("MMIX Simulator");
            Console.WriteLine($"Thomas Holmes 2020. {VersionNumber}");

            if (args.Length < 1)
            {
                Console.WriteLine("A object file must be specified");
                return -1;
            }
            string objectFile = args[0];

            var mmixComputer = new MmixComputer();
            var bytes = Encoding.ASCII.GetBytes(objectFile.Replace(".mmo", string.Empty));

            int pointer = 0x28;
            mmixComputer.Registers[0].Store(1);
            mmixComputer.Registers[1].Store(0x08);

            mmixComputer.AddToMemory(0x08, (ulong)pointer);
            foreach (var b in bytes)
            {
                mmixComputer.Memory[pointer] = b;
                pointer++;
            }

            using (var stream = File.OpenRead(objectFile))
            {
                mmixComputer.LoadStreamIntoMemory(stream);
            }

            while (mmixComputer.Execute() == ExecutionResult.CONTINUE) { }

            Console.WriteLine("Program finished");

            return 0;
        }
    }
}
