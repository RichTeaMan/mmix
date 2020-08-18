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
            var reg = mmixComputer.Registers[tetra.X.ToInt()];

            // multiply by 4 as ops are 4 bytes wide
            var relativeAddress = 4 * (tetra.Y + tetra.Z);

            var address = mmixComputer.PC + relativeAddress;
            reg.StoreLong(address);

            return ExecutionResult.CONTINUE;
        }
    }
}
