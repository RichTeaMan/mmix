using System;

namespace mmixal
{
    public class ExprToken
    {
        public ExprTokenType TokenType { get; }

        public byte Value { get; }

        public IAssemblerVariable AssemblerVariable { get; }

        public ExprToken(IAssemblerVariable assemblerVariable)
        {
            TokenType = ExprTokenType.VARIABLE;
            Value = 0;
            AssemblerVariable = assemblerVariable ?? throw new ArgumentNullException(nameof(assemblerVariable));
        }

        public ExprToken(ExprTokenType tokenType, byte value)
        {
            if (tokenType == ExprTokenType.VARIABLE)
            {
                throw new Exception("Token type cannot be VARIABLE with a byte value.");
            }
            TokenType = tokenType;
            Value = value;
            AssemblerVariable = null;
        }

        public byte FetchByte()
        {
            if (TokenType == ExprTokenType.VARIABLE)
            {
                return AssemblerVariable.FetchByteReference();
            }
            else
            {
                return Value;
            }
        }

        public enum ExprTokenType
        {
            REGISTER,

            CONSTANT,

            VARIABLE
        }
    }
}
