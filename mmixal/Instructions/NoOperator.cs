namespace mmixal.Instructions
{
    public class NoOperator : AbstractInstruction
    {
        public override string[] SupportedSymbols => new[] { string.Empty };
        public override OperatorOutput GenerateBinary(AssemblerState assemblerState, AsmLine asmLine)
        {
            return new OperatorOutput();
        }
    }
}
