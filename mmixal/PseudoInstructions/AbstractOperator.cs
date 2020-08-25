using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace mmixal.PseudoInstructions
{
    public abstract class AbstractOperator
    {
        public abstract string Symbol { get; }
        public abstract OperatorOutput GenerateBinary(string expression);
    }
}
