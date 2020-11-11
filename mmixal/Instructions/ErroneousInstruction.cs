namespace mmixal.Instructions
{
    public class ErroneousInstruction : AbstractInstruction
    {
        public string Symbol { get; }

        public override string[] SupportedSymbols => new string[0];
        public ErroneousInstruction(string opcode)
        {
            Symbol = opcode;
        }
        public override OperatorOutput GenerateBinary(AssemblerState assemblerState, AsmLine asmLine)
        {
            assemblerState.RaiseError($"'{Symbol}' is not implemented.");
            return new OperatorOutput();
        }
    }
}
