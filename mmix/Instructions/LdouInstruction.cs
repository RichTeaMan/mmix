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
            ulong a = mmixComputer.Registers[tetra.Y].ToULong() + mmixComputer.Registers[tetra.Z].ToULong();
            var bytes = mmixComputer.ReadOcta(a);
            reg.Store(bytes);

            return ExecutionResult.CONTINUE;
        }
    }
}
