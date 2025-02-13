﻿using System;
using SkiaSharp;
using Waher.Script.Exceptions;
using Waher.Script.Model;
using Waher.Script.Objects.VectorSpaces;
using Waher.Script.Abstraction.Elements;

namespace Waher.Script.Fractals.ColorModels
{
    /// <summary>
    /// Calculates a palette of random color bands.
    /// 
    /// RandomLinearRGB()                  N = 1024 by default
    /// RandomLinearRGB(N)                 BandSize=16 by default
    /// RandomLinearRGB(N,BandSize)        Seed=random by default.
    /// RandomLinearRGB(N,BandSize,Seed)
    /// </summary>
    public class RandomLinearRGB : FunctionMultiVariate
    {
		public RandomLinearRGB(ScriptNode N, int Start, int Length, Expression Expression)
			: base(new ScriptNode[] { N }, argumentTypes1Scalar, Start, Length, Expression)
		{
		}

		public RandomLinearRGB(ScriptNode N, ScriptNode BandSize, int Start, int Length, Expression Expression)
			: base(new ScriptNode[] { N, BandSize }, argumentTypes2Scalar, Start, Length, Expression)
		{
		}

		public RandomLinearRGB(ScriptNode N, ScriptNode BandSize, ScriptNode Seed, int Start, int Length, Expression Expression)
            : base(new ScriptNode[] { N, BandSize, Seed }, argumentTypes3Scalar, Start, Length, Expression)
        {
		}

		/// <summary>
		/// Evaluates the function.
		/// </summary>
		/// <param name="Arguments">Function arguments.</param>
		/// <param name="Variables">Variables collection.</param>
		/// <returns>Function result.</returns>
		public override IElement Evaluate(IElement[] Arguments, Variables Variables)
		{
			int i = 0;
            int c = Arguments.Length;
            int N;
            int BandSize;
            int Seed;

            if (i < c)
                N = (int)Expression.ToDouble(Arguments[i++].AssociatedObjectValue);
            else
            {
                N = 1024;
                Variables.ConsoleOut.WriteLine("N = " + N.ToString(), Variables);
            }

            if (i < c)
                BandSize = (int)Expression.ToDouble(Arguments[i++].AssociatedObjectValue);
            else
            {
                BandSize = 16;
                Variables.ConsoleOut.WriteLine("BandSize = " + BandSize.ToString(), Variables);
            }

            if (i < c)
                Seed = (int)Expression.ToDouble(Arguments[i++].AssociatedObjectValue);
            else
            {
                lock (gen)
                {
                    Seed = gen.Next();
                }

                Variables.ConsoleOut.WriteLine("Seed = " + Seed.ToString(), Variables);
            }

            return new ObjectVector(CreatePalette(N, BandSize, Seed, this));
        }

		/// <summary>
		/// Default Argument names
		/// </summary>
		public override string[] DefaultArgumentNames
		{
			get
			{
				return new string[] { "N", "BandSize", "Seed" };
			}
		}

		public static SKColor[] CreatePalette(int N, int BandSize, ScriptNode Node)
        {
            return CreatePalette(N, BandSize, null, Node);
        }

        public static SKColor[] CreatePalette(int N, int BandSize, int? Seed, ScriptNode Node)
        {
            if (N <= 0)
                throw new ScriptRuntimeException("N in RandomLinearRGB(N[,BandSize]) has to be positive.", Node);

            if (BandSize <= 0)
                throw new ScriptRuntimeException("BandSize in RandomLinearRGB(N[,BandSize]) has to be positive.", Node);

            SKColor[] Result = new SKColor[N];
            int R1, G1, B1;
            int R2, G2, B2;
            int R, G, B;
            int i, j, c, d;
            int BandSize2 = BandSize / 2;
            Random Generator;

            if (Seed.HasValue)
                Generator = new Random(Seed.Value);
            else
                Generator = gen;

            lock (Generator)
            {
                R2 = Generator.Next(256);
                G2 = Generator.Next(256);
                B2 = Generator.Next(256);

                i = 0;
                while (i < N)
                {
                    R1 = R2;
                    G1 = G2;
                    B1 = B2;

                    R2 = Generator.Next(256);
                    G2 = Generator.Next(256);
                    B2 = Generator.Next(256);

                    c = BandSize;
                    j = N - i;
                    if (c > j)
                        c = j;

                    d = N - i;
                    if (d > c)
                        d = c;

                    for (j = 0; j < d; j++)
                    {
                        R = ((R2 * j) + (R1 * (BandSize - j)) + BandSize2) / BandSize;
                        G = ((G2 * j) + (G1 * (BandSize - j)) + BandSize2) / BandSize;
                        B = ((B2 * j) + (B1 * (BandSize - j)) + BandSize2) / BandSize;

                        if (R > 255)
                            R = 255;

                        if (G > 255)
                            G = 255;

                        if (B > 255)
                            B = 255;

                        Result[i++] = new SKColor((byte)R, (byte)G, (byte)B);
                    }
                }
            }

            return Result;
        }

        private static readonly Random gen = new Random();

        public override string FunctionName
        {
            get { return "RandomLinearRGB"; }
        }
    }
}
