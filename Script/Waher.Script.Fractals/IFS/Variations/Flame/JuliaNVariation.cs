﻿using System;
using Waher.Script.Abstraction.Elements;
using Waher.Script.Model;

namespace Waher.Script.Fractals.IFS.Variations.Flame
{
    public class JuliaNVariation : FlameVariationMultipleParameters
    {
        private readonly double dist;
        private readonly int power;

        public JuliaNVariation(ScriptNode power, ScriptNode dist, int Start, int Length, Expression Expression)
			: base(new ScriptNode[] { power, dist }, new ArgumentType[] { ArgumentType.Scalar, ArgumentType.Scalar },
				  Start, Length, Expression)
		{
			this.power = 0;
            this.dist = 0;
        }

        private JuliaNVariation(int Power, double Dist, ScriptNode power, ScriptNode dist, int Start, int Length, Expression Expression)
			: base(new ScriptNode[] { power, dist }, new ArgumentType[] { ArgumentType.Scalar, ArgumentType.Scalar },
				  Start, Length, Expression)
        {
            this.power = Power;
            this.dist = Dist;
        }

		public override string[] DefaultArgumentNames
		{
			get
			{
				return new string[] { "power", "dist" };
			}
		}

		public override IElement Evaluate(IElement[] Arguments, Variables Variables)
        {
            int Power = (int)Expression.ToDouble(Arguments[0].AssociatedObjectValue);
            double Dist = Expression.ToDouble(Arguments[1].AssociatedObjectValue);

            return new JuliaNVariation(Power, Dist, this.Arguments[0], this.Arguments[1], this.Start, this.Length, this.Expression);
        }

        public override void Operate(ref double x, ref double y)
        {
            int p3;

            lock (this.gen)
            {
                p3 = this.gen.Next(Math.Abs(this.power));
            }

            double a = Math.Atan2(y, x);
            double t = (a + 2 * Math.PI * p3) / this.power;
            double r = Math.Pow(Math.Sqrt(x * x + y * y), this.dist / this.power);
            
            x = r * Math.Cos(t);
            y = r * Math.Sin(t);
        }

        private readonly Random gen = new Random();

        public override string FunctionName
        {
            get { return "JuliaNVariation"; }
        }
    }
}
