namespace mmixal.Instructions
{
    public abstract class AbstractPseudoInstruction : AbstractInstruction
    {
        public override ulong DetermineByteLength(AsmLine asmLine) => 0;
    }
}
