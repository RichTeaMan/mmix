namespace mmixal.PseudoInstructions
{
    public class LocInstruction : AbstractPseudoInstruction
    {
        public override string Symbol => "LOC";

        public override OperatorOutput GenerateBinary(AssemblyInstruction assemblyInstruction, AssemblerState assemblerState)
        {
            // register alias
            ulong location;
            if (TryParseConstant(assemblyInstruction.Expression, out location))
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