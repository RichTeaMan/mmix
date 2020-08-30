namespace mmixal.PseudoInstructions
{
    public class IsInstruction : AbstractPseudoInstruction
    {
        public override string Symbol => "IS";

        public override OperatorOutput GenerateBinary(AssemblyInstruction assemblyInstruction, AssemblerState assemblerState)
        {
            // register alias
            byte registerValue;
            if (!string.IsNullOrWhiteSpace(assemblyInstruction.Label) &&
                TryParseRegister(assemblyInstruction.Expression, out registerValue))
            {
                assemblerState.CreateRegisterAlias(assemblyInstruction.Label, registerValue);
            }
            else
            {
                assemblerState.RaiseError("IS expression must be a register value, eg $1, with a non empty label.");
            }

            return new OperatorOutput();
        }
    }
}