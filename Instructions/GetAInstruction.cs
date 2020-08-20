using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Text;

namespace mmix.Instructions
{
    public class GetAInstruction : AbstractInstruction
    {
        public override byte OpCode => 0xF4;

        public override string Symbol => "GETA";

        public override ExecutionResult ExecuteInstruction(MmixComputer mmixComputer, Tetra tetra)
        {
            // register to store into
            var reg = mmixComputer.Registers[tetra.X];

            // multiply by 4 as ops are 4 bytes wide
            long relativeAddress = 4 * (tetra.Y + tetra.Z);

            // sign check as there isn't a ulong + long overload that handles negatives (I think)
            ulong address;
            if (relativeAddress > 0)
            {
                address = mmixComputer.PC + (ulong)relativeAddress;
            }
            else
            {
                address = mmixComputer.PC - (ulong)(-relativeAddress);
            }
            reg.Store(address);

            return ExecutionResult.CONTINUE;
        }
    }
}
