namespace mmixal.Instructions
{
    public class LocInstruction : AbstractPseudoInstruction
    {
        public override string[] SupportedSymbols => new[] { "LOC" };

        public override OperatorOutput GenerateBinary(AssemblerState assemblerState, AsmLine asmLine)
        {
            // register alias
            ulong location;
            if (TryParseConstant(asmLine.Expr, out location))
            {
                assemblerState.ProgramCounter = location;
            }
            else
            {
                assemblerState.RaiseError("LOC expression must be a constant value.");
            }

            return new OperatorOutput();
        }
    }
}