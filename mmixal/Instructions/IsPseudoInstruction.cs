namespace mmixal.Instructions
{
    public class IsPseudoInstruction : AbstractPseudoInstruction
    {
        public override string[] SupportedSymbols => new[] { "IS" };

        public override OperatorOutput GenerateBinary(AssemblerState assemblerState, AsmLine asmLine)
        {
            if (string.IsNullOrWhiteSpace(asmLine.Label))
            {
                assemblerState.RaiseError("IS must have a label.");
            }
            else
            {
                // TODO only integer constants and register substitutions are supported 
                // register
                if (TryParseRegister(asmLine.Expr, out byte registerRef))
                {
                    assemblerState.DefineVariable(asmLine.Label, new RegisterCompilerVariable(registerRef));
                }
                // constant
                else if (int.TryParse(asmLine.Expr, out int constant))
                {
                    assemblerState.DefineVariable(asmLine.Label, new ByteConstantAssemblerVariable((byte)constant));
                }
                else
                {
                    assemblerState.RaiseError($"IS expression, '{asmLine.Expr}' must be a constant integer or register reference.");
                }
            }

            return new OperatorOutput();
        }
    }
}
