namespace mmixal.PseudoInstructions
{
    public class IsInstruction : AbstractPseudoInstruction
    {
        public override string Symbol => "IS";

        public override OperatorOutput GenerateBinary(AssemblyInstruction assemblyInstruction, AssemblerState assemblerState)
        {
            // register alias
            byte registerValue;
            if (assemblyInstruction.Expression.StartsWith("$") &&
                byte.TryParse(assemblyInstruction.Expression.Replace("$", string.Empty), out registerValue) &&
                !string.IsNullOrWhiteSpace(assemblyInstruction.Label))
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