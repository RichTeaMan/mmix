using System;
using System.Collections.Generic;
using System.Text;

namespace mmix.Instructions
{
    public class TrapInstruction : AbstractInstruction
    {
        public override byte OpCode => 0x00;

        public override string Symbol => "TRAP";

        public override ExecutionResult ExecuteInstruction(MmixComputer mmixComputer, Tetra tetra)
        {
            mmixComputer.rBB = mmixComputer.Registers[255];

            // TODO there is a lot more to be understood here - http://mmix.cs.hm.edu/doc/instructions-en.html#TRAP
            // for now, XYZ == 0 terminates the program.
            if (tetra.Y == Constants.Fputs && tetra.Z == 1)
            {
                // get pointer from $255
                var address = mmixComputer.Registers[255].ToLong();

                byte letter;
                var letters = new List<byte>();
                do
                {
                    letter = mmixComputer.Memory[address];
                    letters.Add(letter);
                    address++;
                } while (letter != 0x00);

                Console.Write(Encoding.ASCII.GetString(letters.ToArray()));
            }
            if (tetra.ToInt() == 0)
            {
                return ExecutionResult.HALTED;
            }

            return ExecutionResult.CONTINUE;
        }
    }
}
