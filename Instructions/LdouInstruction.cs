using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mmix.Instructions
{
    public class LdouInstruction : AbstractInstruction
    {
        public override byte OpCode => 0x8F;

        public override string Symbol => "LDOU";

        public override ExecutionResult ExecuteInstruction(MmixComputer mmixComputer, Tetra tetra)
        {
            var reg = mmixComputer.Registers[tetra.X];
            var a = mmixComputer.Registers[tetra.Y].ToLong();// + mmixComputer.Registers[tetra.Z].ToLong();
            var bytes = mmixComputer.ReadMemory((int)a, 8);
            reg.StoreBytes(bytes);

            return ExecutionResult.CONTINUE;
        }
    }
}
