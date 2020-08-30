namespace mmixal.Variable
{
    public class RegisterAlias : AbstractVariable
    {
        public byte Register { get; }
        public RegisterAlias(string name, byte register)
        {
            Name = name;
            Register = register;
        }
    }
}