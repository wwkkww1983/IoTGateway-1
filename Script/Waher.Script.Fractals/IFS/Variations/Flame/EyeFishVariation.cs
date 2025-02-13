﻿using System;

namespace Waher.Script.Fractals.IFS.Variations.Flame
{
    public class EyeFishVariation : FlameVariationZeroParameters
    {
        public EyeFishVariation(int Start, int Length, Expression Expression)
            : base(Start, Length, Expression)
        {
        }

        public override void Operate(ref double x, ref double y)
        {
            double r = 2 / (1 + Math.Sqrt(x * x + y * y));
            x = r * x;
            y = r * y;
        }

        public override string FunctionName
        {
            get { return "EyeFishVariation"; }
        }
    }
}
