using System;
using System.Text;

namespace mmix
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("MMIX Simulator");

            var mmixComputer = new MmixComputer();
            var bytes = ASCIIEncoding.ASCII.GetBytes("hello");

            int pointer = 0x28;
            mmixComputer.Registers[0].StoreLong(1);
            mmixComputer.Registers[1].StoreLong(0x08);

            mmixComputer.AddToMemory(0x08, (ulong)pointer);
            foreach(var b in bytes)
            {
                mmixComputer.Memory[pointer] = b;
                pointer++;
            }

            mmixComputer.PC = 0x100;

            mmixComputer.AddToMemory(0x100, 0x8fff0100);
            mmixComputer.AddToMemory(0x104, 0x00000701);
            mmixComputer.AddToMemory(0x108, 0xf4ff0003);
            mmixComputer.AddToMemory(0x10c, 0x00000701);
            mmixComputer.AddToMemory(0x110, 0x00000000);
            mmixComputer.AddToMemory(0x114, 0x2c20776f);
            mmixComputer.AddToMemory(0x118, 0x726c640a);
            mmixComputer.AddToMemory(0x11c, 0x00);

            while (mmixComputer.Execute() == ExecutionResult.CONTINUE) { }

            Console.WriteLine("Program finished");
        }
    }
}
