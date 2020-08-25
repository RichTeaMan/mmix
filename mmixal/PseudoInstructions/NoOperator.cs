﻿using System;
using System.Collections.Generic;
using System.Text;

namespace mmixal.PseudoInstructions
{
    public class NoOperator : AbstractOperator
    {
        public override string Symbol => string.Empty;

        public override OperatorOutput GenerateBinary(string expression)
        {
            return new OperatorOutput();
        }
    }
}